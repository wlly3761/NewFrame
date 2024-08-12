using SqlSugar;

namespace Repository.Model;
[SugarTable("user")]
public class MyTableModel
{
    /// <summary>
    /// id
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public string Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }


}