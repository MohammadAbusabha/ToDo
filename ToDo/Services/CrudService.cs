using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using ToDo.Context;
using ToDo.Dto;
using ToDo.Extensions;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;


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
            if(_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                return _todocontext.toDos.FirstOrDefault(x => x.Id == id);
            }
            var c = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _todocontext.toDos.Where(x=>x.Userid==c).FirstOrDefault(x => x.Id == id);
        }
        public void InsertData(DataDto datadto)
        {
            Data data = datadto.ToEntity();

            var c = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            data.Userid = c;
            _todocontext.toDos.Add(data);
            _todocontext.SaveChanges();
        }
        public void UpdateData(DataDto datadto, int id) // fix again
        {
            Data data = datadto.ToEntity();

            //if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value == "Admin")
            //{
            //    _todocontext.toDos.Where(x => x.Id == id).ExecuteUpdate(setters => setters.SetProperty(x => x.Name, datadto.Name)
            //                                                                        .SetProperty(x => x.Description, datadto.Description));
            //    _todocontext.SaveChanges();
            //}

            //var c = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value == "Admin" 
                | data.Userid == Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                _todocontext.toDos.Where(x=>x.Id==id)
                                  .ExecuteUpdate(setters=>setters.SetProperty(x=>x.Name, datadto.Name)
                                                                 .SetProperty(x=>x.Description, datadto.Description));
                _todocontext.SaveChanges();
            }
        }
        public void DeleteData(int id)
        {
            var data = GetData(id);
            _todocontext.toDos.Remove(data);
            _todocontext.SaveChanges();
        }
        public List<Data> List(List<int> ids)
        {
            if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value == "Admin")
            {
                return _todocontext.toDos.Where(x => ids.Contains(x.Id)).ToList();

            }
            var c = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _todocontext.toDos.Where(x=>ids.Contains(x.Id)).Where(x=>x.Userid==c).ToList();
        }
        public List<Data> Search(string name, string desc, bool matchany)
        {
            if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value == "Admin")
            {
                if (matchany)
                {
                    return _todocontext.toDos.Where(x => name.Equals(x.Name) || desc.Equals(x.Description)).ToList();
                }
                return _todocontext.toDos.Where(x => name.Equals(x.Name) && desc.Equals(x.Description)).ToList();
            }

            var c = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (matchany)
            {
                return _todocontext.toDos.Where(x => name.Equals(x.Name) || desc.Equals(x.Description)).Where(x=>x.Userid==c).ToList();
            }
            return _todocontext.toDos.Where(x => name.Equals(x.Name) && desc.Equals(x.Description)).Where(x => x.Userid == c).ToList();
        }
    }
}