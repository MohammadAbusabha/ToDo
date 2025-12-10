using System.Collections.Generic;
using ToDo.Resources;
using ToDo.Entitys;
using System.Threading.Tasks;

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
