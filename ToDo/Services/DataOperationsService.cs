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
using ToDo.Entities;
using ToDo.Interfaces;
using ToDo.Resources;
using ToDo.Resources.Filters;


namespace ToDo.Services
{
    public class DataOperationsService : IDataOperationService
    {
        private readonly DataContext _dataContext;
        private readonly ICurrentUserService _currentUser;
        public DataOperationsService(DataContext todocontext, ICurrentUserService currentUserService)
        {
            _dataContext = todocontext;
            _currentUser = currentUserService;
        }

        // GET //
        public async Task<DataResource> GetData(int id)  
        {
            var data =  await _dataContext.DataTable.FirstOrDefaultAsync(x => x.Id == id);
            return data.Adapt<DataResource>();
        }

        // CREATE //
        public async Task CreateData(CreateDataResource createData)  // takes current user id when creating data / need to change so that admin can choose whos id to use when creating data
        {
            var data = createData.Adapt<Data>();
            data.UserId = _currentUser.CurrentUserId();
            await _dataContext.DataTable.AddAsync(data);
            await _dataContext.SaveChangesAsync();
        }

        // UPDATE // 
        public async Task UpdateData(DataResource updateDataResource) 
        {
            var data = updateDataResource.Adapt<Data>();
            _dataContext.DataTable.Update(data);
            await _dataContext.SaveChangesAsync();
        }

        // DELETE //
        public async Task DeleteData(int id) 
        {
            var data = await _dataContext.DataTable.FindAsync(id);
            _dataContext.DataTable.Remove(data);
            await _dataContext.SaveChangesAsync();
        }

        // LIST //
        public async Task<List<DataResource>> ListData(List<int> ids) 
        {
            var data = await _dataContext.DataTable.Where(x => ids.Contains(x.Id)).ToListAsync();
            return data.Adapt<List<DataResource>>();
        }

        // SEARCH //
        public async Task<List<DataResource>> SearchData(DataFilter filter) 
        {
            if (filter.MatchAny)
            {
                var dataT = await _dataContext.DataTable.Where(x => filter.Name.Equals(x.Name) || filter.Description.Equals(x.Description)).ToListAsync();
                return dataT.Adapt<List<DataResource>>();
            }
            var dataF = await _dataContext.DataTable.Where(x => filter.Name.Equals(x.Name) && filter.Description.Equals(x.Description)).ToListAsync();
            return dataF.Adapt<List<DataResource>>();
        }
    }
}