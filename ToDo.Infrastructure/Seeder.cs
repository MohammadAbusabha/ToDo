using Microsoft.AspNetCore.Identity;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.SpecTest;

namespace ToDo.Infrastructure
{
    public class Seeder
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISpecification<Privilege> _privilegeSpec;
        private readonly IGenericRepository<Privilege> _privilegRepo;
        public Seeder(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ISpecification<Privilege> specification,
            IGenericRepository<Privilege> genericRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _privilegeSpec = specification;
            _privilegRepo = genericRepository;
        }
        public async Task SeedAsync()
        {
            await _userManager.CreateAsync(new ApplicationUser() { UserName = "Admin", Email = "Admin@email.com" }, password: "Abc123!");

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
                if (!await _privilegRepo.ExistAsync(_privilegeSpec.AddCriteria(x => x.Name == privilege)))
                {
                    await _privilegRepo.AddAsync(new Privilege() { Name = privilege });
                }
                //if (!await _context.PrivilegeTable.AnyAsync(x => x.Name == privilege))
                //{
                //    await _context.PrivilegeTable.AddAsync(new Privilege() { Name = privilege });
                //}
            }
            //_context.SaveChanges();
        }
    }
}
