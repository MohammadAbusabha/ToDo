using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid CurrentUserId();
        public string CurrentUserName();
        public string CurrentUserEmail();
        public Task<List<string>> CurrentUserRoles(ApplicationUser user);
    }
}
