using ToDo.Dto;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface IToDo
    {
        public Data GetData(int id, Guid guid);
        public void InsertData(DataDto datadto, Guid id);
        public void UpdateData(DataDto datadto, Guid guid, int id);
        public void DeleteData(int id, Guid guid);
        public List<Data> List(List<int> id, Guid guid);
        public List<Data> Search(string name, string desc, bool matchany, Guid guid);
    }
}
