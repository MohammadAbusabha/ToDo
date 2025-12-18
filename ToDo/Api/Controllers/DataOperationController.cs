using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Infrastructure.Interfaces;
using ToDo.Infrastructure.Resources;
using ToDo.Infrastructure.Resources.Filters;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DataOperationController : ControllerBase
    {
        private readonly IDataOperationService _IdataOperationService;
        public DataOperationController(IDataOperationService iTodo)
        {
            _IdataOperationService = iTodo;
        }


        [Authorize(policy: "Read")]
        [HttpGet("Get data")]
        public async Task<DataResource> GetData(int id)
        {
            return await _IdataOperationService.GetData(id);
        }


        [Authorize(policy: "Write")]
        [HttpPost("Create data")]
        public async Task CreateData(CreateDataResource datadto)
        {
            await _IdataOperationService.CreateData(datadto);
        }


        [Authorize(policy: "Write")]
        [HttpPut("Update data")]
        public async Task UpdateData(DataResource updateDataResource)
        {
            await _IdataOperationService.UpdateData(updateDataResource);
        }


        [Authorize(policy: "Delete")]
        [HttpDelete("Delete data")]
        public async Task DeleteData(int id)
        {
            await _IdataOperationService.DeleteData(id);
        }


        [Authorize(policy: "Read")]
        [HttpPost("List Data")]
        public async Task<List<DataResource>> ListData(List<int> id)
        {
            return await _IdataOperationService.ListData(id);
        }


        [Authorize(policy: "Read")]
        [HttpPost("Search Data")]
        public async Task<List<DataResource>> SearchData(DataFilter filter)
        {
            return await _IdataOperationService.SearchData(filter);
        }
    }
}