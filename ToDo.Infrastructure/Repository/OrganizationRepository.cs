using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.SpecTest;
using ToDo.Infrastructure.Context;

namespace ToDo.Infrastructure.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DataContext _context;
        public OrganizationRepository(DataContext context)
        {
            _context = context;
        }
        public async Task SoftDeleteAsync(ISpecification<Organization> specification)
        {
            var t = await _context.Organizations.Where(specification.Criteria).FirstOrDefaultAsync();
            t.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
