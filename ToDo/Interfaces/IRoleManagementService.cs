using System.Threading.Tasks;
using ToDo.Resources;
using ToDo.Resources.Filters;

namespace ToDo.Interfaces
{
    public interface IRoleManagementService
    {
        public Task<string> RoleAssign(RoleValue filter);
    }
}
