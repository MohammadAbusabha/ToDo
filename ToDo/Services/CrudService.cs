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


namespace ToDo.Services
{
    public class CrudService : IToDo
    {
        private readonly ToDoContext _todocontext;
        public CrudService(ToDoContext todocontext)
        {
            _todocontext = todocontext;
        }
        public Data GetData(int id, Guid guid)
        {
            return _todocontext.toDos.Where(x=>x.Userid==guid).FirstOrDefault(x => x.Id == id);
        }
        public void InsertData(DataDto datadto, Guid id)
        {
            Data data = datadto.ToEntity();
            data.Userid = id;
            _todocontext.toDos.Add(data);
            _todocontext.SaveChanges();
        }
        public void UpdateData(DataDto datadto, Guid guid, int id) // needs fix 
        {
            Data data = datadto.ToEntity();
            if(data.Userid == guid)
            {
                //_todocontext.toDos.Where(x=>x.Id==id).ExecuteUpdate(s=>s.SetProperty(n=>n.Name+"*Updated*"));
                _todocontext.SaveChanges();
            }
        }
        public void DeleteData(int id, Guid guid)
        {
            var data = GetData(id,guid);
            _todocontext.toDos.Remove(data);
            _todocontext.SaveChanges();
        }
        public List<Data> List(List<int> ids, Guid guid)
        {
            return _todocontext.toDos.Where(x=>ids.Contains(x.Id)).Where(x=>x.Userid==guid).ToList();
        }

        public List<Data> Search(string name, string desc, bool matchany, Guid guid)
        {
            if (matchany)
            {
                return _todocontext.toDos.Where(x => name.Equals(x.Name) || desc.Equals(x.Description)).Where(x=>x.Userid==guid).ToList();
            }
            return _todocontext.toDos.Where(x => name.Equals(x.Name) && desc.Equals(x.Description)).Where(x => x.Userid == guid).ToList();
        }
    }
}