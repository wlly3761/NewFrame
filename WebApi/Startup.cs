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
        //必须先构建Controller，否则后面Swagger无法构造
        services.AddControllers();
        // 添加服务到容器中  
        services.AddSwaggerGenExtend();

    }  
  
    // Configure 方法用于配置应用程序的请求处理管道  
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //允许跨域
        app.UseCors("AllowCore");
        // 配置中间件等  
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Core V1");
            c.RoutePrefix = "ApiDoc";
        });
    }  
}