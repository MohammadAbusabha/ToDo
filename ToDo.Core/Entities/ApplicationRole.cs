using Microsoft.AspNetCore.Identity;

namespace ToDo.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public List<RolePrivilege> RolePermissions { get; set; }
        public int Value { get; set; }
    }
}
