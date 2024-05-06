using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Filter;

public class ApiFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // context.Result = new JsonResult(new { code=404, msg="非法请求" });
    }
}