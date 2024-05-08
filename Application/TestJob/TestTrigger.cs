using Microsoft.AspNetCore.Http;
using Quartz;

namespace Application.Test;

public class TestTrigger:IJob
{
    private readonly IHttpContextAccessor _contextAccessor;
    public TestTrigger(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("testtrigger");
    }
}