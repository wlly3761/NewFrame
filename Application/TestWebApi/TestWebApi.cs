using Core.Attribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;

namespace Application.TestWebApi;

[DynamicApi]
public class TestWebApi:ITestWebApi
{
    private readonly ILogger<TestWebApi> _logger;
    public TestWebApi(ILogger<TestWebApi> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public string GetName()
    {
        return "测试";
    }
    [HttpPost]
    public string GetPostName(test test)
    {
        return "成功";
    }
    
}

public class test
{
    public string title { get; set; }
    public string body { get; set; }
    public string userId { get; set; }
}