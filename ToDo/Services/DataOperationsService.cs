using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Context;
using ToDo.Entitys;
using ToDo.Interfaces;
using ToDo.Resources;


namespace ToDo.Services
{
    public class DataOperationsService : IDataOperationService
    {
        private readonly DataContext _todocontext;
        private readonly ClaimsPrincipal _user;
        private readonly Mapping _mapping;
        public DataOperationsService(DataContext todocontext, IHttpContextAccessor httpContextAccessor, Mapping mapping)
        {
            _todocontext = todocontext;
            _user = httpContextAccessor.HttpContext.User;
            _mapping = mapping;
        }
        public async Task<Data> GetData(int id)
        {
            return await _todocontext.DataTable.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateData(DataResource datadto)
        {

            //apply mapping here

            var c = Guid.Parse(_user.FindFirst(ClaimTypes.NameIdentifier).Value);
            data.Userid = c;

            await _todocontext.DataTable.AddAsync(data);
            await _todocontext.SaveChangesAsync();
        }
        public async Task UpdateData(UpdateDataResource updateDataDTO)
        {

            //apply mapping here

            _todocontext.DataTable.Update(data);
            await _todocontext.SaveChangesAsync();
        }
        public async Task DeleteData(int id)
        {
            var data = GetData(id).Result;
            _todocontext.DataTable.Remove(data);
            await _todocontext.SaveChangesAsync();
        }
        public async Task<List<Data>> ListData(List<int> ids)
        {
            return await _todocontext.DataTable.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        public async Task<List<Data>> SearchData(SearchResource searchDTO)
        {
            if (searchDTO.MatchAny)
            {
                return await _todocontext.DataTable.Where(x => searchDTO.Name.Equals(x.Name) || searchDTO.Description.Equals(x.Description)).ToListAsync();
            }
            return await _todocontext.DataTable.Where(x => searchDTO.Name.Equals(x.Name) && searchDTO.Description.Equals(x.Description)).ToListAsync();
        }
    }
}