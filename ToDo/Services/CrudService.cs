using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
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
    public class CrudService : ITodo
    {
        private readonly ToDoContext _todocontext;
        private readonly ClaimsPrincipal _user;
        public CrudService(ToDoContext todocontext, IHttpContextAccessor httpContextAccessor)
        {
            _todocontext = todocontext;
            _user = httpContextAccessor.HttpContext.User;
        }
        public Data GetData(int id)
        {
            return _todocontext.DataTable
                .Where(x => x.Userid == Guid.Parse(_user.FindFirst(ClaimTypes.NameIdentifier).Value))
                .FirstOrDefault(x => x.Id == id);
        }
        public void CreateData(DataDTO datadto)
        {

            Data data = datadto.ToEntity();
            var c = Guid.Parse(_user.FindFirst(ClaimTypes.NameIdentifier).Value);

            data.Userid = c;
            _todocontext.DataTable.Add(data);
            _todocontext.SaveChanges();
        }
        public void UpdateData(UpdateDataDTO updateDataDTO)
        {

            Data data = updateDataDTO.ToEntity();
            _todocontext.DataTable.Where(x=> _user.IsInRole("Admin") | x.Userid == Guid.Parse(_user.FindFirst(ClaimTypes.NameIdentifier).Value)).Where(x => x.Id == updateDataDTO.Id)
                              .ExecuteUpdate(setters => setters.SetProperty(x => x.Name, updateDataDTO.Name)
                                                               .SetProperty(x => x.Description, updateDataDTO.Description));
            _todocontext.SaveChanges();
        }
        public void DeleteData(int id)
        {
            var data = GetData(id);
            _todocontext.DataTable.Remove(data);
            _todocontext.SaveChanges();
        }
        public List<Data> ListData(List<int> ids)
        {
            if (_user.IsInRole("Admin"))
            {
                return _todocontext.DataTable.Where(x => ids.Contains(x.Id)).ToList();

            }
            var c = Guid.Parse(_user.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _todocontext.DataTable.Where(x=>ids.Contains(x.Id)).Where(x=>x.Userid==c).ToList();
        }
        public List<Data> SearchData(SearchDTO searchDTO)
        {

            var c = Guid.Parse(_user.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (searchDTO.MatchAny)
            {
                return _todocontext.DataTable.Where(x => searchDTO.Name.Equals(x.Name) || searchDTO.Description.Equals(x.Description)).Where(x=> _user.IsInRole("Admin") | x.Userid==c).ToList();
            }
            return _todocontext.DataTable.Where(x => searchDTO.Name.Equals(x.Name) && searchDTO.Description.Equals(x.Description)).Where(x => _user.IsInRole("Admin") | x.Userid == c).ToList();
        }
    }
}