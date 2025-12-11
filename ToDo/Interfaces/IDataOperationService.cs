using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entitys;
using ToDo.Resources;

namespace ToDo.Interfaces
{
    public interface IDataOperationService
    {
        public Task<Data> GetData(int id);
        public Task CreateData(DataResource datadto);
        public Task UpdateData(UpdateDataResource updateDataDTO);
        public Task DeleteData(int id);
        public Task<List<Data>> ListData(List<int> id);
        public Task<List<Data>> SearchData(SearchResource searchDTO);
    }
}
