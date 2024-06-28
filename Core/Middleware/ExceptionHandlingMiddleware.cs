using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;

namespace Core.Middleware;

/// <summary>
/// Middleware for handling exceptions
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;  // 用来处理上下文请求
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;  // 日志记录器
    public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger) 
    {
        _next = next;
        _logger=logger;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); //要么在中间件中处理，要么被传递到下一个中间件中去
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex); // 捕获异常了 在HandleExceptionAsync中处理
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";  // 返回json 类型
        _logger.LogInformation("全局异常捕获");
        _logger.LogError(exception,exception.Message);
        ExceptionReturnModel err = new ExceptionReturnModel();
        err.StatusCode = context.Response.StatusCode;
        err.ErrorMessage = exception.Message;
        var errObj = JsonConvert.SerializeObject(err);
        await context.Response.WriteAsync(errObj).ConfigureAwait(false);
    }
    private class ExceptionReturnModel
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}