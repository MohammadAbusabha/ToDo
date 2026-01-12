using ToDo.Core.Entities;
using ToDo.Core.Resources;
using ToDo.Core.Resources.Filters;

namespace ToDo.Core.Interfaces
{
    public interface IDataService
    {
        public Task<List<DataResource>> GetAsync(int id);
        public Task CreateAsync(CreateDataResource createData);
        public Task UpdateAsync(DataResource updateDataResource);
        public Task DeleteAsync(int id);
        public Task<List<DataResource>> ListAsync(List<int> ids);
        public Task<List<DataResource>> SearchAsync(DataFilter filter);
    }
}
