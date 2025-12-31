using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;
using ToDo.Core.Resources;
using ToDo.Core.Resources.Filters;
using ToDo.Core.Interfaces;
using ToDo.Core.SpecTest;
using System.Collections;
using System.Threading.Tasks;
using ToDo.Core.Aggregate;

namespace ToDo.Core.Services
{
    public class DataService : IDataService
    {
        private readonly IGenericRepository<Data> _repo;
        private readonly ICurrentUserService _user;
        private readonly UserManager<ApplicationUser> _userManager;
        public DataService(IGenericRepository<Data> repository, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
        {
            _user = currentUserService;
            _repo = repository;
            _userManager = userManager;
        }

        //private readonly DataContext _dataContext;
        //private readonly ICurrentUserService _currentUser;
        //private readonly IGenericRepository<Data> _repo;
        //private readonly IBaseSpecification<Data> _spec;
        //public DataService(DataContext todocontext, ICurrentUserService currentUserService, IGenericRepository<Data> repo, IBaseSpecification<Data> spec)
        //// Services still broken , still need a way to feed user id that can be bypassed by the admin
        //// it does not make sense that each time i need to create a api to feed it id manually and such 
        //// it should contain logic code only
        //{
        //    _dataContext = todocontext;
        //    _currentUser = currentUserService;
        //    _repo = repo;
        //    _spec = spec;
        //}

        // GET //
        public async Task<IEnumerable<Data>> GetData(int id)
        {
            //var user = await _userManager.FindByIdAsync(_user.UserId.ToString());
            //var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");



            var spec = new Specifications<Data>(x => x.Id == id);
            var data = _repo.GetById(spec);
            return data;

            //var dataId = new GetDataById(id);
            //var userid = new GetdatabyUserId(_currentUser.CurrentId()); // wip



            //var role = _currentUser.TopRole;
            //if (role >= 4)
            //{
            //    return await _repo.GetAsync();
            //}
            //return await _repo.GetAsync(userid);

            //return new Data();
        }

        // CREATE //
        public async Task CreateDataAsync(CreateDataResource createData)
        // takes current user id when creating data
        // need to change so that admin can choose whos id to use when creating data
        {
            var data = createData.Adapt<Data>();
            data.UserId = _user.UserId;
            await _repo.AddAsync(data);
            //var data = createData.Adapt<Data>();
            //data.UserId = _currentUser.UserId;
            //await _dataContext.DataTable.AddAsync(data);
            //await _dataContext.SaveChangesAsync();
        }

        //// UPDATE // 
        //public async Task UpdateData(DataResource updateDataResource)
        //{
        //    var data = updateDataResource.Adapt<Data>();
        //    _dataContext.DataTable.Update(data);
        //    await _dataContext.SaveChangesAsync();
        //}

        //// DELETE //
        //public async Task DeleteData(int id)
        //{
        //    var data = await _dataContext.DataTable.FindAsync(id);
        //    _dataContext.DataTable.Remove(data);
        //    await _dataContext.SaveChangesAsync();
        //}

        //// LIST //
        //public async Task<List<DataResource>> ListData(List<int> ids)
        //{
        //    var data = await _dataContext.DataTable.Where(x => ids.Contains(x.Id)).ToListAsync();
        //    return data.Adapt<List<DataResource>>();
        //}

        //// SEARCH //
        //public async Task<List<DataResource>> SearchData(DataFilter filter)
        //{
        //    if (filter.MatchAny)
        //    {
        //        var dataT = await _dataContext.DataTable.Where(x => filter.Name.Equals(x.Name) || filter.Description.Equals(x.Description)).ToListAsync();
        //        return dataT.Adapt<List<DataResource>>();
        //    }
        //    var dataF = await _dataContext.DataTable.Where(x => filter.Name.Equals(x.Name) && filter.Description.Equals(x.Description)).ToListAsync();
        //    return dataF.Adapt<List<DataResource>>();
        //}
    }
}