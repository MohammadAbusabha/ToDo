using ToDo.Core.Resources;

namespace ToDo.Core.Interfaces
{
    public interface IRoleService
    {
        public Task<string> RoleAssignAsync(RoleResource roleResource);
    }
}
