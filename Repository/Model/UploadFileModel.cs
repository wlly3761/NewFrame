namespace Repository.Model;

public class UploadFileModel
{
    public IEnumerable<UserModel> userModels { get; set; } = new List<UserModel>();
}

public class UserModel
{
    public int userId { get; set; }
    public int orgId { get; set; }
    public int tagId { get; set; }

}