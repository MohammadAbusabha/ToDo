using ToDo.Core.Entities;
using ToDo.Core.Resources;
using ToDo.Core.Resources.Filters;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IDataOperationService
    {
        public Task<Data> GetData(int id);
        public Task CreateData(CreateDataResource datadto);
        public Task UpdateData(DataResource updateDataResource);
        public Task DeleteData(int id);
        public Task<List<DataResource>> ListData(List<int> id);
        public Task<List<DataResource>> SearchData(DataFilter filter);
    }
}
