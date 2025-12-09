using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Security.Claims;
using ToDo.Context;
using ToDo.Dto;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin,User")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ITodo _iTodo;
        public ToDoController(ITodo iTodo)
        {
            _iTodo = iTodo;
        }
        [HttpGet("Get data")]
        public Data GetData(int id) 
        {
            return _iTodo.GetData(id);
        }

        [HttpPost("Insert data")]
        public void CreateData(DataDTO datadto)
        {
            _iTodo.CreateData(datadto);
        }

        [HttpPut("Update data")]
        public void UpdateData(UpdateDataDTO updateDataDTO)
        {
            _iTodo.UpdateData(updateDataDTO);
        }
        [HttpDelete("Delete data")]
        public void DeleteData(int id)
        {
            _iTodo.DeleteData(id);
        }
        [HttpPost("List")]
        public List<Data> ListData(List<int> id)
        {
            return _iTodo.ListData(id);
        }
        [HttpPost("Search")]
        public List<Data> SearchData(SearchDTO searchDTO)
        {
            return _iTodo.SearchData(searchDTO);
        }
    }
}