using Core.Attribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Application.TestWebApi;

[DynamicApi]
public class TestWebApi:ITestWebApi
{
    private readonly ILogger<TestWebApi> _logger;
    public TestWebApi(ILogger<TestWebApi> logger)
    {
        _logger = logger;
    }
    public string GetName()
    {
        int totalCount =0;
        totalCount = int.Parse("测试");
        return "测试";
    }
}