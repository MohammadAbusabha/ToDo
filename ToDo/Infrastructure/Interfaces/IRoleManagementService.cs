using System.Threading.Tasks;
using ToDo.Core.Resources;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IRoleManagementService
    {
        public Task<string> RoleAssign(RoleValue filter);
    }
}
