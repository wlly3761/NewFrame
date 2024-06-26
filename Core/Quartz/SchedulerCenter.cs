using Core.Tools;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Spi;

namespace Core.Quartz;

public class SchedulerCenter
{
    private readonly IConfiguration _configuration;
    private readonly IJobFactory _jobFactory;
    private readonly ISchedulerFactory _schedulerFactory;
    private IScheduler _scheduler;
    public SchedulerCenter(IJobFactory jobFactory, ISchedulerFactory schedulerFactory,IConfiguration configuration)
    {
        _jobFactory = jobFactory;
        _schedulerFactory = schedulerFactory;
        _configuration = configuration;
    }
    public async void StartScheduler()
    {
        //1、从工厂获取调度程序实例
        _scheduler = await _schedulerFactory.GetScheduler();
        
        // 替换默认工厂(存在构造注入的任务调度类必须使用这个，否则调度器无法识别）
        _scheduler.JobFactory = this._jobFactory;
        Type[] types=  AssemblyHelper.GetTypesByAssembly("Application").Where(c => c.GetInterfaces().Contains(typeof(IJobBase)))
            .ToArray();
        if(!types.Any()) return;
        Dictionary<IJobDetail, ITrigger> jobDic = new Dictionary<IJobDetail, ITrigger>();

        foreach (var quartzType in types)
        {
            //3、定义作业详细信息并将其与HelloJob任务相关联
            IJobDetail job = JobBuilder.Create(quartzType)
                .WithIdentity(quartzType.Name, $"JobGroup_{quartzType.Name}")
                .Build();
            //4、配置触发条件：立即触发作业运行，然后每10秒重复一次
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"Trigger_{quartzType.Name}", $"JobGroup_{quartzType.Name}")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(_configuration.GetValue<int>("JobIntervalTime"))
                    .RepeatForever())
                .Build();
            jobDic.Add(job,trigger);
        }
        foreach (var job in jobDic)
        {
            await _scheduler.ScheduleJob(job.Key, job.Value);
        }

        //打开调度器
        await _scheduler.Start();

    }
    public void StopScheduler()
    {
        _scheduler?.Shutdown(true).Wait(30000);
        _scheduler = null;
    }
}