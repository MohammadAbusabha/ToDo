using System.Security.Claims;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IPrivilegeManagementService
    {
        //public Task AddPrivilege(ApplicationUser user); // might use later dont delete also need to be changed
        public Task<List<Claim>> CreatePrivilege(List<int> values);
    }
}
