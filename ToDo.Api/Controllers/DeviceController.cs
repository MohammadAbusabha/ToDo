using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Api.Controllers
{
    [Route("api/Project{projId}/Device")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        [HttpPost]
        public async Task CreateAsync(int projId, DeviceResource resource)
        {
            await _deviceService.CreateAsync(projId, resource);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllByProjAsync(int projId, [FromQuery]PaginationResource pagination)
        {
            return Ok(await _deviceService.GetAllByProjectAsync(projId, pagination));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, [FromQuery]PaginationResource pagination)
        {
            return Ok(await _deviceService.GetByIdAsync(id, pagination));
        }
        [HttpPut]
        public async Task UpdateAsync(int id, DeviceResource deviceResource)
        {
            await _deviceService.UpdateAsync(id, deviceResource);
        }
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _deviceService.DeleteAsync(id);
        }
    }
}
