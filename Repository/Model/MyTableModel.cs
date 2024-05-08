using SqlSugar;

namespace Repository.Model;
[SugarTable("mytable")]
public class MyTableModel
{
    /// <summary>
    /// id
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    public int age { get; set; }


}