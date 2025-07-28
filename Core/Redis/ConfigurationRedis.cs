using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Util;
using StackExchange.Redis;

namespace Core.Redis;
/// <summary>
/// 注册Redis
/// </summary>
public static class ConfigurationRedis
{
    public static void AddRedisSetup(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var redisCongruation = configuration.GetSection("Redis").GetValue<string>("Configuration");
        if (redisCongruation == null) return;
        serviceCollection.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisCongruation));
    }
}
