using ToDo.Core.Resources;
using ToDo.Core.SpecTest;

namespace ToDo.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class // one repo for full app
    {
        //public Task<List<T>> GetAllAsync();
        public Task<List<T>> GetAllBySpecAsync(PaginationResource pagination, ISpecification<T> specification);
        public Task CreateAsync(T entity);
        public Task<T> GetAsync(ISpecification<T> specification);
        public Task<bool> ExistAsync(ISpecification<T> specification);
        public Task UpdateAsync(object obj, ISpecification<T> specification);
        public Task DeleteAsync(ISpecification<T> specification);
    }
}