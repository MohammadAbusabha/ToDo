using Microsoft.AspNetCore.Identity;
using System;
using ToDo.Interfaces;
using ToDo.Entitys;

namespace ToDo.IdentityEntity_s
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}