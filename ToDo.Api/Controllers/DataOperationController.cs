using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Core.Resources.Filters;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DataOperationController : ControllerBase
    {
        private readonly IDataService _IdataOperationService;
        public DataOperationController(IDataService iTodo)
        {
            _IdataOperationService = iTodo;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetData(int id)
        {
            var data = await _IdataOperationService.GetAsync(id);
            return Ok(data);
        }
        [Authorize()]
        [HttpPost]
        public async Task CreateData(CreateDataResource datadto)
        {
            await _IdataOperationService.CreateAsync(datadto);
        }

        [HttpPut]
        public async Task UpdateData(DataResource updateDataResource)
        {
            await _IdataOperationService.UpdateAsync(updateDataResource);
        }

        [HttpDelete("{id}")]
        public async Task DeleteData(int id)
        {
            await _IdataOperationService.DeleteAsync(id);
        }

        [HttpPost("list")]
        public async Task<List<DataResource>> ListData(List<int> id)
        {
            return await _IdataOperationService.ListAsync(id);
        }

        [HttpPost("search")]
        public async Task<List<DataResource>> SearchData(DataFilter filter)
        {
            return await _IdataOperationService.SearchAsync(filter);
        }
    }
}