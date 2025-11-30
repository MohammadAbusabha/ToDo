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
        private readonly ToDoContext _todocontext;
        private readonly UserManager<ApplicationUser> _usermanager;
        public ToDoController(IToDo iToDo, ToDoContext toDoContext, UserManager<ApplicationUser> userManager)
        {
            _iToDo = iToDo;
            _todocontext = toDoContext;
            _usermanager = userManager;
        }
        [HttpGet]
        public Data Get(int id)
        {
            var currid = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _iToDo.GetData(id, Guid.Parse(currid));
        }

        [HttpPost]
        public void Post(DataDto datadto)
        {
            var currid = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _iToDo.InsertData(datadto, Guid.Parse(currid));

        }

        [HttpPut]
        public void Update(DataDto datadto, int id)
        {
            var currid = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _iToDo.UpdateData(datadto, Guid.Parse(currid), id);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            var currid = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _iToDo.DeleteData(id, Guid.Parse(currid));
        }
        [HttpPost("List")]
        public List<Data> ListData(List<int> id)
        {
            var currid = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _iToDo.List(id, Guid.Parse(currid));
        }
        [HttpPost("Search")]
        public List<Data> SearchData(string name, string desc, bool matchany)
        {
            var currid = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _iToDo.Search(name, desc, matchany, Guid.Parse(currid));
        }
    }
}