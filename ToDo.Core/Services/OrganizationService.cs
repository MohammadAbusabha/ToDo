using Mapster;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Core.SpecTest;

namespace ToDo.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ISpecification<Organization> _specification;
        private readonly IGenericRepository<Organization> _repo;
        public OrganizationService(ISpecification<Organization> specification,
            IGenericRepository<Organization> genericRepository)
        {
            _specification = specification;
            _repo = genericRepository;
        }
        public async Task<List<Organization>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<Organization> GetByIdAsync(int id)
        {
            return await _repo.GetAsync(_specification.AddCriteria(x => x.Id == id));
        }
        public async Task CreateAsync(OrganizationResource organizationResource)
        {
            await _repo.AddAsync(organizationResource.Adapt<Organization>());
        }
        public async Task UpdateAsync(UpdateOrgResource updateOrgResource)
        {
           var spec = _specification.AddCriteria(x => x.Id == updateOrgResource.Id);
           await _repo.UpdateAsync(updateOrgResource.Adapt<Organization>(), spec);
        }
        public async Task DeleteByIdAsync(SoftDeleteResource softDelete) // wip
        {
            var spec = _specification.AddCriteria(x => x.Id == softDelete.Id);
            await _repo.SoftDeleteAsync(softDelete.Adapt<Organization>(), spec);
        }
    }
}
