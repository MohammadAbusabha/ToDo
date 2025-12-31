using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> op) : base(op)
        {
        }
        public DbSet<Data> DataTable { get; set; }
        public DbSet<Privilege> PrivilegeTable { get; set; }
        public DbSet<PrivilegeRole> PrivilegeRoles { get; set; }
        //public DbSet<Permissions> PermissionsTable { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}
    }
}