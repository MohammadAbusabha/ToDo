using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using ToDo.Entitys;

namespace ToDo.IdentityEntity_s
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Data> Data { get; set; }
    }
}