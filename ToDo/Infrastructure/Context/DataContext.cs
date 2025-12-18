using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> op) : base(op)
        {
        }
        public DbSet<Data> DataTable { get; set; }
        public DbSet<Privilege> Privilege { get; set; }
        public DbSet<RolePrivilege> RolePrivilege { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<RolePrivilege>().HasKey(sc => new { sc.RoleId, sc.PrivilegeId });
        }
    }
}