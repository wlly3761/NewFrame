using Repository.Model;

namespace Application.System.MyTable;

public interface IMyTable
{
     List<MyTableModel> GetList(int id);
}