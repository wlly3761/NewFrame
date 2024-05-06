using Blog.BaseConfigSerivce.DynamicAPi;
using Core.AutoInjectService;
using Core.Filter;
using Core.Swagger;


namespace WebApi;

public class Startup
{
    // ConfigureServices 方法用于配置应用程序的服务  
    public void ConfigureServices(IServiceCollection services)  
    {  
        //跨域
        services.AddCors(option =>
        {
            option.AddPolicy(name: "AllowCore", x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });
        });
        //添加Controller请求过滤器
        services.AddMvc(options => { options.Filters.Add<ApiFilter>(); });
        //注入http请求上下文
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //添加httpClient后可以在项目任何地址构造函数中注入使用。
        services.AddHttpClient();
        //必须先构建Controller，否则后面Swagger无法构造，同时构建动态WebApi
        services.AddControllers().AddDynamicWebApi();
        // 添加Swagger
        services.AddSwaggerGenExtend();
        //自动注入WebApi服务接口
        services.AutoRegistryService();
        //注册SignalR
        services.AddSignalR();

    }  
  
    // Configure 方法用于配置应用程序的请求处理管道  
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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
    }  
}