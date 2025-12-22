using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Resources;
using ToDo.Core.Resources.Filters;
using ToDo.Infrastructure.BaseSpecifications;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Infrastructure.Services
{
    public class DataOperationsService : IDataOperationService
    {
        private readonly DataContext _dataContext;
        private readonly ICurrentUserService _currentUser;
        private readonly IDataRepository<Data> _repo;
        public DataOperationsService(DataContext todocontext, ICurrentUserService currentUserService, IDataRepository<Data> repo)
        // Services still broken , still need a way to feed user id that can be bypassed by the admin
        // it does not make sense that each time i need to create a api to feed it id manually and such 
        // it should contain logic code only
        {
            _dataContext = todocontext;
            _currentUser = currentUserService;
            _repo = repo;
        }

        // GET //
        public async Task<Data> GetData(int id)
        {
            var t = new GetDataById(id);
            return await _repo.GetData(t);
        }

        // CREATE //
        public async Task CreateData(CreateDataResource createData)
        // takes current user id when creating data
        // need to change so that admin can choose whos id to use when creating data
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