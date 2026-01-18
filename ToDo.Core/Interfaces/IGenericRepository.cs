using ToDo.Core.SpecTest;

namespace ToDo.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class // one repo for full app
    {
        public Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllBySpecAsync(ISpecification<T> specification = null);
        public Task AddAsync(T entity);
        public Task<T> GetAsync(ISpecification<T> specification = null);
        public Task<bool> ExistAsync(ISpecification<T> specification);
        public Task UpdateAsync(T entity, ISpecification<T> specification);
        public Task DeleteAsync(T entity);
        public Task SoftDeleteAsync(T entity, ISpecification<T> specification);
    }
}