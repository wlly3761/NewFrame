using Core.DynamicAPi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.BaseConfigSerivce.DynamicAPi;

public static class AutoApiEx
{
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