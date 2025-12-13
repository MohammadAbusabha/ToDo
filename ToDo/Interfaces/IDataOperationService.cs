using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entitys;
using ToDo.Resources;

namespace ToDo.Interfaces
{
    public interface IDataOperationService
    {
        public Task<DataResource> GetData(int id);
        public Task CreateData(DataResource datadto);
        public Task UpdateData(UpdateDataResource updateDataResource);
        public Task DeleteData(int id);
        public Task<List<DataResource>> ListData(List<int> id);
        public Task<List<DataResource>> SearchData(MatchanyResource searchDTO);
    }
}
