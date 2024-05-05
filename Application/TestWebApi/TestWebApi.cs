using Core.Attribute;

namespace Application.TestWebApi;

[DynamicApi(ServiceLifeCycle = "Scoped")]
public class TestWebApi:ITestWebApi
{
    public string GetName()
    {
        return "测试";
    }
}