using Mapster;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Core.SpecTest;

namespace ToDo.Core.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ISpecification<Device> _spec;
        private readonly IGenericRepository<Device> _repository;
        public DeviceService(IGenericRepository<Device> repository,
            ISpecification<Device> specification)
        {
            _repository = repository;
            _spec = specification;
        }
        public async Task CreateAsync(int id, DeviceResource deviceResource)
        {
            var data = deviceResource.Adapt<Device>();
            data.ProjectId = id;
            await _repository.CreateAsync(data);
        }
        public async Task<List<DeviceResource>> GetAllByProjectAsync(int id, PaginationResource pagination)
        {
            var devices = await _repository.GetAllBySpecAsync(pagination, _spec.AddCriteria(x => x.ProjectId == id));
            return devices.Adapt<List<DeviceResource>>();
        }
        public async Task<List<DeviceResource>> GetByIdAsync(int id, PaginationResource pagination)
        {
            var device = await _repository.GetAllBySpecAsync(pagination, _spec.AddCriteria(x => x.Id == id));
            return device.Adapt<List<DeviceResource>>();
        }
        public async Task UpdateAsync(int id, DeviceResource deviceResource)
        {
            var spec = _spec.AddCriteria(x => x.Id == id);
            await _repository.UpdateAsync(deviceResource, spec);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(_spec.AddCriteria(x=>x.Id == id));
        }
    }
}
