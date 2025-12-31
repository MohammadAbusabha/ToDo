using Microsoft.EntityFrameworkCore;
using ToDo.Core.SpecTest;
using ToDo.Infrastructure.Context;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure.ServiceTest
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetById(ISpecification<T> specification = null)
        {
            return _context.Set<T>().Where(specification.Criteria);
        }
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            _context.SaveChanges();
        }
    }
}