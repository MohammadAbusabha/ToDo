using ToDo.Core.SpecTest;

namespace ToDo.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class // one repo for full app
    {
        Task<List<T>> GetByIdAsync(ISpecification<T> specification = null);
        public Task AddAsync(T entity);
    }
}