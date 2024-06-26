using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Core.SqlSugar;

/// <summary>
///     SqlSugar 依赖注入
/// </summary>
public static class SqlSugarInit
{
    public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        services.AddScoped<ISqlSugarClient>(o =>
        {
            var client = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = configuration.GetValue<string>("DBConnection"),
                    DbType = DbType.MySql,
                    IsAutoCloseConnection = true,
                    ConfigId = "1"
                },
                db =>
                {
                    db.Aop.OnLogExecuting = (sql, p) =>
                    {
                        Console.WriteLine(1+sql);
                    };
                });
            return client;
        });
    }
}