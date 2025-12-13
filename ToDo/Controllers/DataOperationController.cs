using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entitys;
using ToDo.Interfaces;
using ToDo.Resources;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DataOperationController : ControllerBase
    {
        private readonly IDataOperationService _iTodo;
        public DataOperationController(IDataOperationService iTodo)
        {
            _iTodo = iTodo;
        }


        [Authorize(policy: "CanGet")]
        [HttpGet("Get data")]
        public async Task<DataResource> GetData(int id)
        {
            return await _iTodo.GetData(id);
        }


        [Authorize(policy: "CanCreate")]
        [HttpPost("Create data")]
        public async Task CreateData(DataResource datadto)
        {
            await _iTodo.CreateData(datadto);
        }


        [Authorize(policy: "CanUpdate")]
        [HttpPut("Update data")]
        public async Task UpdateData(UpdateDataResource updateDataResource)
        {
            await _iTodo.UpdateData(updateDataResource);
        }


        [Authorize(policy: "CanDelete")]
        [HttpDelete("Delete data")]
        public async Task DeleteData(int id)
        {
            await _iTodo.DeleteData(id);
        }


        [Authorize(policy: "CanList")]
        [HttpPost("List Data")]
        public async Task<List<DataResource>> ListData(List<int> id)
        {
            return await _iTodo.ListData(id);
        }


        [Authorize(policy: "CanSearch")]
        [HttpPost("Search Data")]
        public async Task<List<DataResource>> SearchData(MatchanyResource searchDTO)
        {
            return await _iTodo.SearchData(searchDTO);
        }
    }
}