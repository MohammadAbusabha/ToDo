using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Entities;
using ToDo.Core.Resources;

namespace ToDo.Core.Interfaces
{
    public interface IOrganizationService
    {
        public Task<List<OrganizationResource>> GetAllAsync(PaginationResource pagination);
        public Task<OrganizationResource> GetByIdAsync(int id);
        public Task CreateAsync(OrganizationResource organizationResource);
        public Task UpdateAsync(UpdateOrgResource updateOrgResource);
        public Task DeleteByIdAsync(SoftDeleteResource softDelete);
    }
}
