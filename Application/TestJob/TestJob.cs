using Quartz;

namespace Application.TestJob;

public class TestJob:IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("testjob");
        ;
    }
}