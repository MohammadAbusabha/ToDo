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
        public DataOperationsService(DataContext todocontext)
        {
            _todocontext = todocontext;
        }

        // GET //
        public async Task<DataResource> GetData(int id) 
        {
            var data =  await _todocontext.DataTable.FirstOrDefaultAsync(x => x.Id == id);
            return data.Adapt<DataResource>();
        }

        // CREATE //
        public async Task CreateData(DataResource dataresourcedto) 
        {

            var data = dataresourcedto.Adapt<Data>();
            await _todocontext.DataTable.AddAsync(data);
            await _todocontext.SaveChangesAsync();
        }

        // UPDATE // 
        public async Task UpdateData(UpdateDataResource updateDataResource) 
        {
            var data = updateDataResource.Adapt<Data>();
            _todocontext.DataTable.Update(data);
            await _todocontext.SaveChangesAsync();
        }
        public async Task DeleteData(int id) 
        {
            var data = await _todocontext.DataTable.FindAsync(id);
            _todocontext.DataTable.Remove(data);
            await _todocontext.SaveChangesAsync();
        }

        // LIST //
        public async Task<List<DataResource>> ListData(List<int> ids) 
        {
            var data =  await _todocontext.DataTable.Where(x => ids.Contains(x.Id)).ToListAsync();
            return data.Adapt<List<DataResource>>();
        }

        // SEARCH //
        public async Task<List<DataResource>> SearchData(MatchanyResource searchDTO) 
        {
            if (searchDTO.MatchAny)
            {
                var dataT = await _todocontext.DataTable.Where(x => searchDTO.Name.Equals(x.Name) || searchDTO.Description.Equals(x.Description)).ToListAsync();
                return dataT.Adapt<List<DataResource>>();
            }
            var dataF = await _todocontext.DataTable.Where(x => searchDTO.Name.Equals(x.Name) && searchDTO.Description.Equals(x.Description)).ToListAsync();
            return dataF.Adapt<List<DataResource>>();
        }
    }
}