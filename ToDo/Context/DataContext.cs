using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Entitys;

namespace ToDo.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public DataContext(DbContextOptions<DataContext>op, IHttpContextAccessor httpContextAccessor) : base(op)  
        {
            _contextAccessor = httpContextAccessor;
        }
        public DbSet<Data> DataTable { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Data>().HasQueryFilter(x=> _contextAccessor.HttpContext.User.IsInRole("Admin") 
            | x.Userid == Guid.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }
    }
}