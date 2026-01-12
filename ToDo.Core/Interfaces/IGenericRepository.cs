using ToDo.Core.SpecTest;

namespace ToDo.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class // one repo for full app
    {
        Task<List<T>> GetAllAsync(ISpecification<T> specification = null);
        public Task AddAsync(T entity);
        public Task<T> GetAsync(ISpecification<T> specification = null);
        public Task<bool> ExistAsync(ISpecification<T> specification);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}