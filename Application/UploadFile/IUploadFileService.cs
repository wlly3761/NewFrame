using Repository.Model;

namespace Application.UploadFile;

public interface IUploadFileService
{
    Task<string> BatchUploadFileAsync(UploadFileModel models);
}