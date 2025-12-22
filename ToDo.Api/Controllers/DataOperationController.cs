using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Entities;
using ToDo.Core.Resources;
using ToDo.Core.Resources.Filters;
using ToDo.Infrastructure.Interfaces;

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
        [HttpGet("{id}")]
        public async Task<Data> GetData(int id)
        {
            return await _IdataOperationService.GetData(id);
        }


        [Authorize(policy: "Write")]
        [HttpPost]
        public async Task CreateData(CreateDataResource datadto)
        {
            await _IdataOperationService.CreateData(datadto);
        }


        [Authorize(policy: "Write")]
        [HttpPut]
        public async Task UpdateData(Core.Resources.DataResource updateDataResource)
        {
            await _IdataOperationService.UpdateData(updateDataResource);
        }


        [Authorize(policy: "Delete")]
        [HttpDelete("{id}")] // do this to other api
        public async Task DeleteData(int id)
        {
            await _IdataOperationService.DeleteData(id);
        }


        [Authorize(policy: "Read")]
        [HttpPost("list")]
        public async Task<List<Core.Resources.DataResource>> ListData(List<int> id)
        {
            return await _IdataOperationService.ListData(id);
        }


        [Authorize(policy: "Read")]
        [HttpPost("search")]
        public async Task<List<Core.Resources.DataResource>> SearchData(DataFilter filter)
        {
            return await _IdataOperationService.SearchData(filter);
        }
    }
}