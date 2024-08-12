using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Autofac;

namespace Core.AutofacExtentions;

public class RBACModule:Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //1、动态加载程序集
        var assembly = Assembly.Load("Application");

        //2.注册
        builder.RegisterAssemblyTypes(assembly)
            //自动加载接口
            .AsImplementedInterfaces();
        base.Load(builder);
    }
}