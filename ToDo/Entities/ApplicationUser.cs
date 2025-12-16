using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Data> Data { get; set; }
    }
}
