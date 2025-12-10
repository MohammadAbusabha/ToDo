using System.Threading.Tasks;
using ToDo.Resources;
using ToDo.IdentityEntity_s;

namespace ToDo.Interfaces
{
    public interface IRoleManagementService
    {
        public Task<string> RoleAssign(RoleResource roleDTO);
    }
}
