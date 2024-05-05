using System.Reflection;
using Core.Attribute;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Blog.BaseConfigSerivce.DynamicAPi;

public class AutoApiControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        //使用注解的方式进行识别
        var hasAnnotation = Attribute.IsDefined(typeInfo, typeof(DynamicApiAttribute));
        //判断是否使用了指定注解
        if (hasAnnotation)
            if (typeInfo is { IsInterface: false, IsAbstract: false, IsGenericType: false, IsPublic: true })
                return true;

        return false;
    }
}