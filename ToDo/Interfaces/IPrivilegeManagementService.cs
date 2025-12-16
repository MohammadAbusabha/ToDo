using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Entities;
using ToDo.Resources;

namespace ToDo.Interfaces
{
    public interface IPrivilegeManagementService
    {
        //public Task AddPrivilege(ApplicationUser user); // might use later dont delete also need to be changed
        public Task<List<Claim>> CreatePrivilege(List<int> values);
    }
}
