// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
//
// app.MapGet("/", () => "Hello World!");
//
// app.Run();

using WebApi;

public class Program
{
    public static void Main(string[] args)  
    {  
        CreateHostBuilder(args).Build().Run();  
    }  
  
    public static IHostBuilder CreateHostBuilder(string[] args) =>  
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>  
            {  
                webBuilder.UseStartup<Startup>(); // 这里配置Startup类  
            });
}
  