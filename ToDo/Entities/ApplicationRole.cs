using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public List<RolePrivilege> RolePermissions { get; set; }
        public int Value { get; set; }
    }
}
