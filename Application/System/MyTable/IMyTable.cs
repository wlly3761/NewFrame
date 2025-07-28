using Repository.Model;

namespace Application.System.MyTable;

public interface IMyTable
{
     Task<List<MyTableModel>> GetList(string id);
}