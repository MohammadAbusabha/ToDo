using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Infrastructure.Services
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly DataContext _context;
        public DataRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Data> GetData(IBaseSpecification<Data> spec)
        {
            return await _context.Set<Data>()
                .Where(spec.Criteria)
                .FirstOrDefaultAsync();
        }
    }
}
