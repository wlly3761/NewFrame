using System.Reflection;
using System.Runtime.Loader;
using Blog.BaseConfigSerivce.DynamicAPi;
using Core.AutoInjectService;
using Core.Filter;
using Core.Middleware;
using Core.Quartz;
using Core.SignalR;
using Core.SqlSugar;
using Core.Swagger;
using Core.Tools;
using NLog;
using NLog.Web;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using ReZero;
using ReZero.SuperAPI;

namespace WebApi;

public static class Init
{
    public static void InitializationApplication(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        //构建服务
        BuildServices(builder);
        //配置
        var app = builder.Build();
        Configure(app);
        app.Run();
    }

    private static void BuildServices(WebApplicationBuilder builder)
    {
        builder.Logging.AddNLog("NLog.config");
        builder.Host.UseNLog();
        //跨域
        builder.Services.AddCors(option =>
        {
            option.AddPolicy(name: "AllowCore", x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });
        });
        //添加Controller请求过滤器
        builder.Services.AddMvc(options => { options.Filters.Add<ApiFilter>(); });
        //注入http请求上下文
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //添加httpClient后可以在项目任何地址构造函数中注入使用。
        builder.Services.AddHttpClient();
        //必须先构建Controller，否则后面Swagger无法构造，同时构建动态WebApi
        builder.Services.AddControllers().AddDynamicWebApi();
        // 添加Swagger
        builder.Services.AddSwaggerGenExtend();
        //自动注入WebApi服务接口
        builder.Services.AutoRegistryService();
        //注册SignalR
        builder.Services.AddSignalR();
        //添加SqlSugar服务
        builder.Services.AddSqlsugarSetup(builder.Configuration);
        //注入Quartz任何工厂及调度工厂
        builder.Services.AddSingleton<IJobFactory, QuartzJobFactory>();
        builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        //注入调度中心
        builder.Services.AddSingleton<SchedulerCenter>();
        //批量注入任务调度作业类
        Type[] types = AssemblyHelper.GetTypesByAssembly("Application")
            .Where(c => c.GetInterfaces().Contains(typeof(IJobBase))).ToArray();
        foreach (var serviceType in types)
        {
            builder.Services.AddSingleton(serviceType);
        }

        //注册ReZero.Api
        // builder.Services.AddReZeroServices(api =>
        // {
        //     
        //     
        //     //有重载可换json文件 (断点看一下apiObj.DatabaseOptions.ConnectionConfig有没有字符串进来)
        //     var apiObj = SuperAPIOptions.GetOptions("rezero.json"); 
        //
        //     //IOC业务等所有需要的所有集程集 有多个dll就写多个
        //     var assemblyList = new Assembly[] { Assembly.GetExecutingAssembly(),
        //         AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Application"))
        //          };
        //
        //     apiObj!.DependencyInjectionOptions = new DependencyInjectionOptions(assemblyList);
        //
        //     //启用超级API
        //     api.EnableSuperApi(apiObj);
        //
        // });
      
        //添加OpenTelemetry
        const string serviceName = "roll-dice";
        builder.Logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName))
                .AddConsoleExporter();
        });
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter());
        
    }

    private static void Configure(WebApplication app)
    {
        //配置全局异常处理
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); //配置MVC控制器路由  
        });
        //允许跨域
        app.UseCors("AllowCore");
        //使用Swagger  
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Core V1");
            c.RoutePrefix = "ApiDoc";
        });
        //使用SignalR
        app.UseEndpoints(endpoints => { endpoints.MapHub<CommunicationHub>("/communicationHub"); });
        //任务调度
        //获取调度中心实例
        var quartz = app.Services.GetRequiredService<SchedulerCenter>();
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            quartz.StartScheduler(); //项目启动后启动调度中心
        });
        app.Lifetime.ApplicationStopped.Register(() =>
        {
            quartz.StopScheduler(); //项目停止后关闭调度中心
        });
    }
}