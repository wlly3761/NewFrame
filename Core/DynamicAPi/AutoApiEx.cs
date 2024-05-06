using Core.DynamicAPi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.BaseConfigSerivce.DynamicAPi;
/// <summary>
/// 自动添加ApiController
/// </summary>
public static class AutoApiEx
{
    /// <summary>
    /// 添加动态WebApi
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.ConfigureApplicationPartManager(applicationPartManager =>
        {
            applicationPartManager.FeatureProviders.Add(new AutoApiControllerFeatureProvider());
        });

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Conventions.Add(new AutoApiApplicationModelConvention());
        });

        return builder;
    }

    public static IMvcCoreBuilder AddDynamicWebApi(this IMvcCoreBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.ConfigureApplicationPartManager(applicationPartManager =>
        {
            applicationPartManager.FeatureProviders.Add(new AutoApiControllerFeatureProvider());
        });

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Conventions.Add(new AutoApiApplicationModelConvention());
        });

        return builder;
    }
}