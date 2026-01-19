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
        private readonly IOrganizationRepository _orgRepo;
        public OrganizationService(ISpecification<Organization> specification,
            IGenericRepository<Organization> genericRepository,
            IOrganizationRepository organizationRepository)
        {
            _specification = specification;
            _repo = genericRepository;
            _orgRepo = organizationRepository;
        }
        public async Task<List<OrganizationResource>> GetAllAsync(PaginationResource pagination)
        {
            var organizations = await _repo.GetAllBySpecAsync(pagination, _specification.AddCriteria(null));
            return organizations.Adapt<List<OrganizationResource>>();
        }
        public async Task<OrganizationResource> GetByIdAsync(int id)
        {
            var organization = await _repo.GetAsync(_specification.AddCriteria(x => x.Id == id));
            return organization.Adapt<OrganizationResource>();
        }
        public async Task CreateAsync(OrganizationResource organizationResource)
        {
            await _repo.CreateAsync(organizationResource.Adapt<Organization>());
        }
        public async Task UpdateAsync(UpdateOrgResource updateOrgResource)
        {
           var spec = _specification.AddCriteria(x => x.Id == updateOrgResource.Id);
           await _repo.UpdateAsync(updateOrgResource, spec);
        }
        public async Task DeleteByIdAsync(SoftDeleteResource softDelete)
        {
            var spec = _specification.AddCriteria(x => x.Id == softDelete.Id);
            await _orgRepo.SoftDeleteAsync(spec);
        }
    }
}
