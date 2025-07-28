using Application.Helper;
using Core.Attribute;
using Repository.Model;
using SqlSugar;

namespace Application.System.MyTable;
[DynamicApi]
public class MyTable : IMyTable
{
    private readonly ISqlSugarClient _sugarClient;
    private readonly RedisHelper _redisHelper;
    public MyTable(ISqlSugarClient sugarClient, RedisHelper redisHelper)
    {
        _sugarClient = sugarClient;
        _redisHelper = redisHelper;
    }
    public async Task<List<MyTableModel>> GetList(string id)
    {
        var data = await _redisHelper.SetStringAsync("mytable", "chat");
        var value = await _redisHelper.GetStringAsync("mytable");
        return await _sugarClient.Queryable<MyTableModel>().Where(c => c.Id.ToString() == id).ToListAsync();
    }
}