using ToDo.Dto;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface IToDo
    {
        public Data GetData(int id);
        public void InsertData(DataDto datadto);
        public void UpdateData(DataDto datadto, int id);
        public void DeleteData(int id);
        public List<Data> List(List<int> id);
        public List<Data> Search(string name, string desc, bool matchany);
    }
}
