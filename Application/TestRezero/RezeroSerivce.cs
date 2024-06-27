using Core.Attribute;
using Microsoft.AspNetCore.Mvc;
using Models;
using ReZero.DependencyInjection;
using ReZero.SuperAPI;
using SqlSugar;
using DataTable = System.Data.DataTable;

namespace Application.TestRezero;
[Api(200100, GroupName = "TestRezero")]
public class RezeroSerivce:IRezeroSerivce
{
    //属性注入只支持API，非API用构造函数
    [DI]
    public ISqlSugarClient? db { get; set; }
    [ApiMethod("测试方法")]
    public List<Sys_log> GetTestList(PageModel pageModel)
    {
        List<Sys_log> list = null;
        list = list.Where(c => c.Exception =="stake").ToList();
        return db!.Queryable<Sys_log>().ToPageList(pageModel.PageIndex, pageModel.PageSize);
    }
}