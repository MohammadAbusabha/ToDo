using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDo.Context;
using ToDo.Dto;
using ToDo.Enums;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;

namespace ToDo.Services
{
    public class UserTypeService : IUserType
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserTypeService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = contextAccessor;
        }
        public async Task<string> RoleSelect(string s)
        {
            var resault = await _roleManager.RoleExistsAsync(s);

            if (resault)// check if user has role 
            {
                return await RoleAssign(s);
            } 
            else if (resault != true) // if role dont exist Create it
            {
                ApplicationRole role = new ApplicationRole();

                switch (s)
                {
                    case "Admin":
                        role.Name = UserTypeEnum.Admin.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(s);
                        return "Role Created!";
                    case "User":
                        role.Name = UserTypeEnum.User.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(s);
                        return "Role Created!";
                    case "Viewer":
                        role.Name = UserTypeEnum.Viewer.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(s);
                        return "Role Created!";
                    case "Guest":
                        role.Name = UserTypeEnum.Guest.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(s);
                        return "Role Created!";
                    case "Test":
                        role.Name = UserTypeEnum.Test.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(s);
                        return "Role Created!";
                }
            }
            return "Incorrect role";
        }
        public async Task<string> RoleAssign(string s)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
            var dbuser = _userManager.FindByNameAsync(user.Value).Result;

            if (_userManager.GetRolesAsync(dbuser) != null) // Turn off if you want to change the method function to update/change role
            {
                return "User has a Role";
            }

            await _userManager.AddToRoleAsync(dbuser, s);
            return "Role granted";
        }
    }
}