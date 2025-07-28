using StackExchange.Redis;

namespace Application.Helper;
/// <summary>
/// Redis帮助类
/// </summary>
public class RedisHelper
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    public RedisHelper(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = _redis.GetDatabase();
    }
    #region String操作
    public async Task<bool> SetStringAsync(string key, string value, int db = 0) => await _redis.GetDatabase(db).StringSetAsync(key, value);
    public async Task<string> GetStringAsync(string key, int db = 0) => await _redis.GetDatabase(db).StringGetAsync(key);
    #endregion
}
