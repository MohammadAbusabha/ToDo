using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Core.Resources.Filters;
using ToDo.Core.SpecTest;

namespace ToDo.Core.Services
{
    public class DataService : IDataService
    {
        private readonly IGenericRepository<Data> _dataRepo;
        private readonly ICurrentUserService _user;
        private readonly ISpecification<Data> _spec;
        public DataService(
            IGenericRepository<Data> repository,
            ICurrentUserService currentUserService,
            ISpecification<Data> specification)
        {
            _user = currentUserService;
            _dataRepo = repository;
            _spec = specification;
        }

        // GET //
        public async Task<List<DataResource>> GetAsync(int id, PaginationResource pagination)
        {
            var data = await _dataRepo.GetAllBySpecAsync(pagination, _spec.AddCriteria(x => x.Id == id));
            return data.Adapt<List<DataResource>>();
        }

        // CREATE //
        public async Task CreateAsync(CreateDataResource createData)
        // takes current user id when creating data
        // need to change so that admin can choose whos id to use when creating data
        {
            var data = createData.Adapt<Data>();
            data.UserId = _user.UserId;
            await _dataRepo.CreateAsync(data);
        }

        // UPDATE // 
        public async Task UpdateAsync(DataResource updateDataResource)
        {
            var spec = _spec.AddCriteria(x => x.Id == updateDataResource.Id);
            await _dataRepo.UpdateAsync(updateDataResource, spec);
        }

        // DELETE //
        public async Task DeleteAsync(int id)
        {
            var spec = _spec.AddCriteria(x => x.Id == id);
            await _dataRepo.DeleteAsync(spec);
        }

        // LIST //
        public async Task<List<DataResource>> ListAsync(List<int> ids)
        {
            var list = new List<Data>();
            foreach (var id in ids)
            {
                var spec = _spec.AddCriteria(x => x.Id == id);
                var data = await _dataRepo.GetAsync(spec);
                list.Add(data);
            }
            return list.Adapt<List<DataResource>>();
        }

        // SEARCH //
        public async Task<List<DataResource>> SearchAsync(DataFilter filter, PaginationResource pagination)
        {
            if (filter.MatchAny)
            {
                var data = await _dataRepo.GetAllBySpecAsync(pagination, _spec.AddCriteria(x => x.Name == filter.Name || x.Description == filter.Description));
                return data.Adapt<List<DataResource>>();
            }
            var data1 = await _dataRepo.GetAllBySpecAsync(pagination ,_spec.AddCriteria(x => x.Name == filter.Name && x.Description == filter.Description));
            return data1.Adapt<List<DataResource>>();
        }
    }
}