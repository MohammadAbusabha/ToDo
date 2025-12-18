using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ToDo.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public List<RolePrivilege> RolePermissions { get; set; }
        public int Value { get; set; }
    }
}
