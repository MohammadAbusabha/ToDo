using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ToDo.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Data> Data { get; set; }
    }
}
