using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ToDo.IdentityEntity_s
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        //public ApplicationUser ApplicationUser { get; set;  }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
