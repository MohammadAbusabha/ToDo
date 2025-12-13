using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using ToDo.Enums;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Resources;

namespace ToDo.Services
{
    [AllowAnonymous]
    public class RoleManagementService : IRoleManagementService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleManagementService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task CreateRole(string rolename)
        {
            var resault = await _roleManager.RoleExistsAsync(rolename);

            if (!resault)
            {
                ApplicationRole role = new ApplicationRole();
                var enumRoleNames = typeof(Role).GetEnumNames();
                foreach (var enumRoleName in enumRoleNames)
                {
                    if (enumRoleName == rolename)
                    {
                        role.Name = enumRoleName;
                        await _roleManager.CreateAsync(role);
                        return;
                    }
                }
                throw new Exception("Incorrect Role");
            }
        }
        public async Task<string> RoleAssign(RoleResource roleDTO)
        {
            await CreateRole(roleDTO.RoleName);
            var user = _userManager.FindByNameAsync(roleDTO.UserName).Result;
            await _userManager.AddToRoleAsync(user, roleDTO.RoleName);
            return "Role granted";
        }
    }
}