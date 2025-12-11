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
        private readonly IHttpContextAccessor _contextAccessor;
        public DataContext(DbContextOptions<DataContext> op, IHttpContextAccessor httpContextAccessor) : base(op)
        {
            _contextAccessor = httpContextAccessor;
        }
        public DbSet<Data> DataTable { get; set; }
    }
}