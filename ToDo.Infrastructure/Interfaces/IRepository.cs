using ToDo.Core.Entities;
using ToDo.Core.Resources;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class // one repo for full app
    {
        Task<T> GetAsync(IBaseSpecification<T> spec);
    }
}
