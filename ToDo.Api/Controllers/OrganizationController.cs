using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSAsync(int id)
        {
            return Ok(await _organizationService.GetByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationResource pagination)
        {
            return Ok(await _organizationService.GetAllAsync(pagination));
        }
        [HttpPost]
        public async Task Create(OrganizationResource organizationResource)
        {
            await _organizationService.CreateAsync(organizationResource);
        }
        [HttpPut]
        public async Task Update(UpdateOrgResource updateOrgResource)
        {
            await _organizationService.UpdateAsync(updateOrgResource);
        }
        [HttpDelete]
        public async Task DeleteAsync(SoftDeleteResource softDeleteResource)
        {
            await _organizationService.DeleteByIdAsync(softDeleteResource);
        }
    }
}
