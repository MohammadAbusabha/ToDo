using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using ToDo.Enums;
using ToDo.Entities;
using ToDo.Interfaces;
using ToDo.Resources;
using ToDo.Resources.Filters;
using Mapster;

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
        public async Task<string> CreateRole(int value)
        {
            // checks if role is valid
            string rolename = Enum.GetName(typeof(Role), value);
            if (rolename == null)
            {
                throw new Exception("Role is not valid!");
            }

            // creates the role if it does not exist
            var resault = _roleManager.RoleExistsAsync(rolename).Result;
            if (!resault)
            {
                //var role = rolename.Adapt<ApplicationRole>(); // mapster breaks since rolename is a string 
                ApplicationRole role = new()
                {
                    Name = rolename,
                    Value = value
                };
                await _roleManager.CreateAsync(role);
            }
            return rolename;
        }
        public async Task<string> RoleAssign(RoleValue filter)
        {
            var role = await CreateRole(filter.Value);
            var user = _userManager.FindByNameAsync(filter.UserName).Result;
            await _userManager.AddToRoleAsync(user, role);
            return "Role granted";
        }
    }
}