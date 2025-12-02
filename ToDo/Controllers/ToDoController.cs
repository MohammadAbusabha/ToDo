using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDo _iToDo;
        public ToDoController(IToDo iToDo)
        {
            _iToDo = iToDo;
        }
        [HttpGet]
        public Data Get(int id)
        {
            return _iToDo.GetData(id);
        }

        [HttpPost]
        public void Post(DataDto datadto)
        {
            _iToDo.InsertData(datadto);

        }

        [HttpPut]
        public void Update(DataDto datadto, int id)
        {
            _iToDo.UpdateData(datadto, id);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            _iToDo.DeleteData(id);
        }
        [HttpPost("List")]
        public List<Data> ListData(List<int> id)
        {
            return _iToDo.List(id);
        }
        [HttpPost("Search")]
        public List<Data> SearchData(string name, string desc, bool matchany)
        {
            return _iToDo.Search(name, desc, matchany);
        }
    }
}