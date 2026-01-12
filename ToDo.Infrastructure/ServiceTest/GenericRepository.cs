using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
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
        public async Task<List<T>> GetAllAsync(ISpecification<T> specification)
        {
            return await _context.Set<T>().Where(specification.Criteria).ToListAsync();
        }
        public async Task AddAsync(T entity)
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
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        
    }
}