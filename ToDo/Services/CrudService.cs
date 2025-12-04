using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ToDo.Context;
using ToDo.Dto;
using ToDo.Extensions;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ToDo.Services
{
    public class CrudService : IToDo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ToDoContext _todocontext;
        public CrudService(ToDoContext todocontext, IHttpContextAccessor httpContextAccessor)
        {
            _todocontext = todocontext;
            _httpContextAccessor = httpContextAccessor;
        }
        public Data GetData(int id)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user.IsInRole("Admin"))
            {
                return _todocontext.ToDos.FirstOrDefault(x => x.Id == id);
            }
            return _todocontext.ToDos
                .Where(x => x.Userid == Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value))
                .FirstOrDefault(x => x.Id == id);
        }
        public void InsertData(DataDto datadto)
        {
            var user = _httpContextAccessor.HttpContext.User;
            Data data = datadto.ToEntity();
            var c = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);

            data.Userid = c;
            _todocontext.ToDos.Add(data);
            _todocontext.SaveChanges();
        }
        public void UpdateData(DataDto datadto, int id)
        {
            var user = _httpContextAccessor.HttpContext.User;
            Data data = datadto.ToEntity();
            _todocontext.ToDos.Where(x=> user.IsInRole("Admin") | x.Userid == Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value)).Where(x => x.Id == id)
                              .ExecuteUpdate(setters => setters.SetProperty(x => x.Name, datadto.Name)
                                                               .SetProperty(x => x.Description, datadto.Description));
            _todocontext.SaveChanges();
        }
        public void DeleteData(int id)
        {
            var data = GetData(id);
            _todocontext.ToDos.Remove(data);
            _todocontext.SaveChanges();
        }
        public List<Data> List(List<int> ids)
        {
            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                return _todocontext.ToDos.Where(x => ids.Contains(x.Id)).ToList();

            }
            var c = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _todocontext.ToDos.Where(x=>ids.Contains(x.Id)).Where(x=>x.Userid==c).ToList();
        }
        public List<Data> Search(string name, string desc, bool matchany)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var c = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (matchany)
            {
                return _todocontext.ToDos.Where(x => name.Equals(x.Name) || desc.Equals(x.Description)).Where(x=> user.IsInRole("Admin") | x.Userid==c).ToList();
            }
            return _todocontext.ToDos.Where(x => name.Equals(x.Name) && desc.Equals(x.Description)).Where(x => user.IsInRole("Admin") | x.Userid == c).ToList();
        }
    }
}