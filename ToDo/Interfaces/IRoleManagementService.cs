using System.Threading.Tasks;
using ToDo.Resources;

namespace ToDo.Interfaces
{
    public interface IRoleManagementService
    {
        public Task<string> RoleAssign(RoleResource roleDTO);
    }
}
