using ToDo.Core.Resources;

namespace ToDo.Core.Interfaces
{
    public interface IRoleService
    {
        public Task CreateRoleWithPrivilegeAsync(RolePrivilegeResource rolePrivilegeResource);
        public Task AddToRoleAsync(RoleResource roleResource);
    }
}
