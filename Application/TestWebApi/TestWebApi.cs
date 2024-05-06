using Core.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace Application.TestWebApi;

[DynamicApi]
public class TestWebApi:ITestWebApi
{
    public string GetName()
    {
        return "测试";
    }
}