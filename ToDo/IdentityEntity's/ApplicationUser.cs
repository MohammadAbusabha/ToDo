using Microsoft.AspNetCore.Identity;
using System;
using ToDo.Models;

namespace ToDo.IdentityEntity_s
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid RoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
    }
}