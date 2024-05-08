using Core.Attribute;
using Repository.Model;
using SqlSugar;

namespace Application.System.MyTable;
[DynamicApi]
public class MyTable:IMyTable
{
    private readonly ISqlSugarClient _sugarClient;
    public MyTable(ISqlSugarClient sugarClient)
    {
        _sugarClient = sugarClient;
    }
    public List<MyTableModel> GetList(int id)
    {
       return _sugarClient.Queryable<MyTableModel>().Where(c=>c.id==id).ToList();
    }
}