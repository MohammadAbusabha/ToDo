using ToDo.Core.SpecTest;

namespace ToDo.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class // one repo for full app
    {
        IEnumerable<T> GetById(ISpecification<T> specification = null);
        public Task AddAsync(T entity);
    }
}