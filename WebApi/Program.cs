// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
//
// app.MapGet("/", () => "Hello World!");
//
// app.Run();

using NLog.Web;
using WebApi;

public class Program
{
    public static void Main(string[] args)  
    {  
        // 配置 NLog  
        var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();  
  
        try  
        {  
            var host = CreateHostBuilder(args).Build();  
            // 确保应用程序的依赖注入容器创建完毕，以便 NLog 可以捕获这些日志  
            logger.Info("Initializing application...");  
            // 运行应用程序  
            host.Run();  
        }  
        catch (Exception ex)  
        {  
            // 记录启动过程中的异常  
            logger.Error(ex, "Stopped program because of exception");  
            throw;  
        }  
        finally  
        {  
            // 确保 NLog 被正确关闭  
            NLog.LogManager.Shutdown();  
        }  
    }  
  
    public static IHostBuilder CreateHostBuilder(string[] args) =>  
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>  
            {  
                webBuilder.UseStartup<Startup>(); // 这里配置Startup类  
            });
}
  