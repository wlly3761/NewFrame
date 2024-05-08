using Quartz;
using Quartz.Spi;

namespace Core.Quartz;

public class SchedulerCenter
{
    private readonly IJobFactory _jobFactory;
    private readonly ISchedulerFactory _schedulerFactory;
    private IScheduler _scheduler;
    public SchedulerCenter(IJobFactory jobFactory, ISchedulerFactory schedulerFactory)
    {
        _jobFactory = jobFactory;
        _schedulerFactory = schedulerFactory;
    }
    public async void StartScheduler()
    {
        //1、从工厂获取调度程序实例
        _scheduler = await _schedulerFactory.GetScheduler();

        // 替换默认工厂
        _scheduler.JobFactory = this._jobFactory;

       

        //3、定义作业详细信息并将其与HelloJob任务相关联
        IJobDetail job = JobBuilder.Create()
            .WithIdentity("testJob", "HelloJobGroup")
            .Build();
        //4、配置触发条件：立即触发作业运行，然后每10秒重复一次
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "HelloJobGroup")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
            .Build();
        IJobDetail job2 = JobBuilder.Create<TestTrigger>()
            .WithIdentity("testTrigger", "HelloJobGroup2")
            .Build();
        ITrigger trigger2 = TriggerBuilder.Create()
            .WithIdentity("trigger2", "HelloJobGroup2")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
            .Build();

        //5、将作业与触发条件添加到调度实例并进行关联
        await _scheduler.ScheduleJob(job, trigger);
        await _scheduler.ScheduleJob(job2, trigger2);
        //2、打开调度器
        await _scheduler.Start();
        // 保持主线程活动，以便调度器可以运行  
        await Task.Delay(-1);  

    }
    public void StopScheduler()
    {
        _scheduler?.Shutdown(true).Wait(30000);
        _scheduler = null;
    }
}