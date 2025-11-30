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
        public DbSet<Data> toDos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>().HasOne<Data>(d=>d.Data).WithOne(ad=>ad.ApplicationUser).HasForeignKey<Data>(ada=>ada.Userid);
            builder.Entity<Data>().HasOne<ApplicationUser>(a=>a.ApplicationUser).WithMany(b=>b.datas).HasForeignKey(b=>b.Userid);
        }   
    }
}