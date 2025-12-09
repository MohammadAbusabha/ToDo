using Microsoft.AspNetCore.Identity;
using System;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.IdentityEntity_s
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}