using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid CurrentUserId();
        public string CurrentUserName();
        public string CurrentUserEmail();
        public Task<List<string>> CurrentUserRoles(ApplicationUser user);
    }
}
