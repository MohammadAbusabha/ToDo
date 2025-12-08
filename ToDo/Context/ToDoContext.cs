using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ToDo.IdentityEntity_s;
using ToDo.Models;

namespace ToDo.Context
{
    public class ToDoContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ToDoContext(DbContextOptions<ToDoContext>op) : base(op) 
        {
        }
        public DbSet<Data> DataTable { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasOne<ApplicationRole>(o => o.ApplicationRole)
                .WithMany(o=>o.ApplicationUser)
                .HasForeignKey(o=>o.RoleId);
        }
    }
}