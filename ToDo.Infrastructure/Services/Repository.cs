using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.Context;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        public async Task<T?> GetAsync(IBaseSpecification<T> spec) // should take T / also names for repo should end with async and be generic
        {
            return await _context.Set<T>()
                .Where(spec.Criteria)
                .FirstOrDefaultAsync();
        }
    }
}
