using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Core.Resources;

namespace ToDo.Core.Interfaces
{
    public interface IProjectService
    {
        public Task CreateAsync(int id, ProjectResource projectResource);
        public Task<List<ProjectResource>> GetAllByOrgAsync(int id, PaginationResource pagination);
        public Task UpdateAsync(int id, ProjectResource projectResource);
        public Task DeleteAsync(int id);
    }
}
