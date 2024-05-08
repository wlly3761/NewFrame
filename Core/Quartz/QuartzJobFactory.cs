using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Core.Quartz;

/// <summary>
/// 手动实现Quartz定时器工厂 避免Jobs无法注入service的问题
/// </summary>
public class QuartzJobFactory : IJobFactory
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;
 
    public QuartzJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
 
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        IServiceScope serviceScope = _serviceProvider.CreateScope();
        IJob job;
        try
        {
            Type jobType = bundle.JobDetail.JobType;
            job = (IJob) serviceScope.ServiceProvider.GetService(jobType);
        }
        catch
        {
            serviceScope.Dispose();
            throw;
        }
 
        return job;
    }
 
    /// <summary>
    /// 清理Quartz任务
    /// </summary>
    public void ReturnJob(IJob job)
    {
        if (job == null)
        {
            return;
        }
 
        IDisposable disposable = job as IDisposable;
        disposable?.Dispose();
    }
}