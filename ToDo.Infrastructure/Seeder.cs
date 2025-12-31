using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Context;

namespace ToDo.Infrastructure
{
    public class Seeder
    {
        private readonly DataContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public Seeder(DataContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            await _userManager.CreateAsync(new ApplicationUser() { UserName = "Admin"}, password:"Abc123!");

            var roles = new List<string>()
            {
                "Admin",
                "User",
                "Guest",
                "Editor",
                "RoleManager",
            };
            var privileges = new List<string>()
            {
                "Owner",
                "Read",
                "Write",
                "Delete",
                "Manager",
            };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole() { Name = role });
                }
            }
            foreach (var privilege in privileges)
            {
                if (!await _context.PrivilegeTable.AnyAsync(x => x.Name == privilege))
                {
                    await _context.PrivilegeTable.AddAsync(new Privilege() { Name = privilege });
                }
            }
            _context.SaveChanges();
        }
    }
}
