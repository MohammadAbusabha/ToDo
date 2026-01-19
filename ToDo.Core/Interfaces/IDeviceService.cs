using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Resources;

namespace ToDo.Core.Interfaces
{
    public interface IDeviceService
    {
        public Task CreateAsync(int id, DeviceResource deviceResource);
        public Task<List<DeviceResource>> GetAllByProjectAsync(int id, PaginationResource pagination);
        public Task<List<DeviceResource>> GetByIdAsync(int id, PaginationResource pagination);
        public Task UpdateAsync(int id, DeviceResource deviceResource);
        public Task DeleteAsync(int id);
    }
}
