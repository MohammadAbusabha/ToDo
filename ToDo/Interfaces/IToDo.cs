using System.Collections.Generic;
using ToDo.Dto;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ITodo
    {
        public Data GetData(int id);
        public void CreateData(DataDTO datadto);
        public void UpdateData(UpdateDataDTO updateDataDTO);
        public void DeleteData(int id);
        public List<Data> ListData(List<int> id);
        public List<Data> SearchData(SearchDTO searchDTO);
    }
}
