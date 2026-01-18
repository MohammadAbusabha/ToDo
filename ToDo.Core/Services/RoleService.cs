using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Core.SpecTest;

namespace ToDo.Core.Services
{
    [AllowAnonymous]
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IGenericRepository<PrivilegeRole> _rolePrivilegeRepo;
        private readonly IGenericRepository<Privilege> _privilegeRepo;
        private readonly ISpecification<Privilege> _privilegSpec;
        private readonly ISpecification<PrivilegeRole> _privilegRoleSpec;
        public RoleService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IGenericRepository<PrivilegeRole> rolePrivilegeRepo,
            IGenericRepository<Privilege> privilegeRepo,
            ISpecification<Privilege> specification,
            ISpecification<PrivilegeRole> privilegeroleSpec)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _rolePrivilegeRepo = rolePrivilegeRepo;
            _privilegeRepo = privilegeRepo;
            _privilegSpec = specification;
            _privilegRoleSpec = privilegeroleSpec;
        }

        public async Task CreateRoleWithPrivilegeAsync(RolePrivilegeResource rolePrivilegeResource)
        {
            // Get Role ID //
            string roleId;

            if (!await _roleManager.RoleExistsAsync(rolePrivilegeResource.RoleName))
            {
                var role = new ApplicationRole() { Name = rolePrivilegeResource.RoleName };
                await _roleManager.CreateAsync(role);
                roleId = await _roleManager.GetRoleIdAsync(role);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(rolePrivilegeResource.RoleName);
                roleId = await _roleManager.GetRoleIdAsync(role);
            }

            // Get Privilege ID'S //
            List<Guid> privilegeIDS = new List<Guid>();
            foreach (var privilegeName in rolePrivilegeResource.Privilege)
            {
                var privilege = await _privilegeRepo.GetAsync(_privilegSpec.AddCriteria(x => x.Name == privilegeName));
                privilegeIDS.Add(privilege.Id);
            }

            // Save Role With Privilege's //
            foreach (var privilegeID in privilegeIDS)
            {
                var privilegeRole = new PrivilegeRole()
                {
                    RolesId = Guid.Parse(roleId),
                    PrivilegesId = privilegeID,
                };
                if (await _rolePrivilegeRepo.ExistAsync(_privilegRoleSpec.AddCriteria(x => x.RolesId == privilegeRole.RolesId && x.PrivilegesId == privilegeRole.PrivilegesId)))
                {
                    throw new Exception("Exist");
                }
                await _rolePrivilegeRepo.AddAsync(privilegeRole);
            }
        }
        public async Task AddToRoleAsync(RoleResource roleResource)
        {
            var role = await _roleManager.FindByNameAsync(roleResource.RoleName);
            var user = await _userManager.FindByNameAsync(roleResource.UserName);

            if (role != null && user != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
                return;
            }
            throw new Exception("Role or User dont exist");
        }
    }
}