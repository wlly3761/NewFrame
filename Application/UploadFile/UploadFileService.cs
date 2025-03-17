using Core.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Model;

namespace Application.UploadFile;

[DynamicApi]
public class UploadFileService:IUploadFileService
{
    private readonly IHttpContextAccessor context;
    public UploadFileService(IHttpContextAccessor context)
    {
        this.context = context;
    }
    [HttpPost("BatchUploadFile")]
    public async Task<string> BatchUploadFileAsync([FromForm] UploadFileModel models)
    {
        var form = await context.HttpContext!.Request.ReadFormAsync();
        var file = form.Files["file"];
        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        string filePath = Path.Combine(uploadPath, file.FileName);

        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fs);
        }

        context.HttpContext.Response.StatusCode = 200;
        await context.HttpContext.Response.WriteAsync($"File '{file.FileName}' uploaded successfully.");
        return file.FileName;
    }

}