using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Api.Controllers
{
    [Route("api/Organization{orgId}/Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost]
        public async Task CreateAsync(int orgId, ProjectResource projectResource)
        {
            await _projectService.CreateAsync(orgId, projectResource);
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(int orgId, [FromQuery]PaginationResource pagination)
        {
            return Ok(await _projectService.GetAllByOrgAsync(orgId, pagination));
        }
        [HttpPut]
        public async Task UpdateAsync(int id, ProjectResource projectResource)
        {
            await _projectService.UpdateAsync(id, projectResource);
        }
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _projectService.DeleteAsync(id);
        }
    }
}
