using System.Threading.Tasks;
using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IDataRepository<T> where T : class
    {
        Task<Data> GetData(IBaseSpecification<Data> spec);
    }
}
