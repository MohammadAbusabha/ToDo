using Mapster;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Core.SpecTest;

namespace ToDo.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _repo;
        private readonly ISpecification<Project> _spec;
        public ProjectService(ISpecification<Project> specification,
            IGenericRepository<Project> genericRepository)
        {
            _repo = genericRepository;
            _spec = specification;
        }
        public async Task CreateAsync(int id, ProjectResource projectResource)
        {
            var project = projectResource.Adapt<Project>();
            project.OrganizationId = id;
            await _repo.CreateAsync(project);
        }
        public async Task<List<ProjectResource>> GetAllByOrgAsync(int id, PaginationResource pagination)
        {
            var spec = _spec.AddCriteria(x => x.OrganizationId == id);
            var projects = await _repo.GetAllBySpecAsync(pagination, spec);
            return projects.Adapt<List<ProjectResource>>();
        }
        public async Task UpdateAsync(int id, ProjectResource projectResource)
        {
            var spec = _spec.AddCriteria(x=>x.Id == id);
            await _repo.UpdateAsync(projectResource, spec);
        }
        public async Task DeleteAsync(int id)
        {
            var spec = _spec.AddCriteria(x => x.Id == id);
            await _repo.DeleteAsync(spec);
        }
    }
}
