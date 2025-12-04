using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.IdentityEntity_s;
using ToDo.Models;

namespace ToDo.Context
{
    public class ToDoContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ToDoContext(DbContextOptions<ToDoContext>op) : base(op) 
        {
        }
        public DbSet<Data> ToDos { get; set; } 
    }
}