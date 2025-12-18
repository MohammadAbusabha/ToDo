using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Infrastructure.Resources;
using ToDo.Infrastructure.Resources.Filters;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IDataOperationService
    {
        public Task<DataResource> GetData(int id);
        public Task CreateData(CreateDataResource datadto);
        public Task UpdateData(DataResource updateDataResource);
        public Task DeleteData(int id);
        public Task<List<DataResource>> ListData(List<int> id);
        public Task<List<DataResource>> SearchData(DataFilter filter);
    }
}
