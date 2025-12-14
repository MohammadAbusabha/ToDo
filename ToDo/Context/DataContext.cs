using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ToDo.Entitys;
using ToDo.IdentityEntity_s;

namespace ToDo.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> op) : base(op)
        {
        }
        public DbSet<Data> DataTable { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasMany(a=>a.Data).WithOne(a=>a.User).HasForeignKey(a=>a.Userid);
        }
    }
}