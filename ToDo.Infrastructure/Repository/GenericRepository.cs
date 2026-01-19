using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Core.SpecTest;
using ToDo.Infrastructure.Context;

namespace ToDo.Infrastructure.ServiceTest
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await _context.Set<T>().ToListAsync();
        //}
        public async Task<List<T>> GetAllBySpecAsync(PaginationResource pagination, ISpecification<T> specification)
        {
            //return await _context.Set<T>().Where(specification.Criteria).ToListAsync();
            var query = _context.Set<T>().AsQueryable();
            //pagenum, amount return/exist, amount in page
            var page = pagination.Page;
            var pageSize = pagination.PageSize;
            var totalAmount = query.CountAsync().Result;
            var pageCount = (int)Math.Ceiling((double)totalAmount / pageSize);
            var skip = (page) * pageSize;

            if (specification.Criteria == null)
            {
                return await query
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();
            }
            return await query.Where(specification.Criteria)
                              .Skip(skip)
                              .Take(pageSize)
                              .ToListAsync();
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<T> GetAsync(ISpecification<T> specification)
        {
            return await _context.Set<T>().FirstAsync(specification.Criteria);
        }
        public async Task<bool> ExistAsync(ISpecification<T> specification)
        {
            return await _context.Set<T>().AnyAsync(specification.Criteria);
        }
        public async Task UpdateAsync(object obj, ISpecification<T> specification)
        {
            var trackedEntity =_context.Set<T>().SingleOrDefault(specification.Criteria);
            if(trackedEntity != null)
            {
                _context.Entry(trackedEntity).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                return;
            }
            throw new Exception("Entity does not exist!");
        }
        public async Task DeleteAsync(ISpecification<T> specification)
        {
            await _context.Set<T>().Where(specification.Criteria).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

    }
}