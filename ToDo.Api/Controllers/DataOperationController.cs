using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataOperationController : ControllerBase
    {
        private readonly IDataService _IdataOperationService;
        public DataOperationController(IDataService iTodo)
        {
            _IdataOperationService = iTodo;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetData(int id)
        {
            var data = _IdataOperationService.GetData(id);
            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        public async Task CreateData(CreateDataResource datadto)
        {
            await _IdataOperationService.CreateDataAsync(datadto);
        }

        //[Authorize(policy: "CanWrite")]
        //[HttpPut]
        //public async Task UpdateData(DataResource updateDataResource)
        //{
        //    await _IdataOperationService.UpdateData(updateDataResource);
        //}

        //[Authorize(policy: "CanDelete")]
        //[HttpDelete("{id}")]
        //public async Task DeleteData(int id)
        //{
        //    await _IdataOperationService.DeleteData(id);
        //}

        //[Authorize(policy: "CanRead")]
        //[HttpPost("list")]
        //public async Task<List<DataResource>> ListData(List<int> id)
        //{
        //    return await _IdataOperationService.ListData(id);
        //}

        //[Authorize(policy: "CanRead")]
        //[HttpPost("search")]
        //public async Task<List<DataResource>> SearchData(DataFilter filter)
        //{
        //    return await _IdataOperationService.SearchData(filter);
        //}
    }
}