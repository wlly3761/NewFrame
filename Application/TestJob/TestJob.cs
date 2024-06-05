using Core.Quartz;
using Quartz;

namespace Application.TestJob;

public class TestJob:IJobBase
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("testjob");
    }
}