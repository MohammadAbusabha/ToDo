using System.Threading.Tasks;
using ToDo.Infrastructure.Resources;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IRoleManagementService
    {
        public Task<string> RoleAssign(RoleValue filter);
    }
}
