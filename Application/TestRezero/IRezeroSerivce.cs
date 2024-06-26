using Microsoft.AspNetCore.Mvc;
using Models;
using SqlSugar;

namespace Application.TestRezero;

public interface IRezeroSerivce
{
    List<Sys_log> GetTestList([FromBody]PageModel pageModel);
}