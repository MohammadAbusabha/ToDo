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

            if (resault != true) // if role dont exist Create it
            {
                ApplicationRole role = new ApplicationRole();

                switch (s)
                {
                    case "Admin":
                        role.Name = UserTypeEnum.Admin.ToString();
                        await _roleManager.CreateAsync(role);
                        break;
                    case "User":
                        role.Name = UserTypeEnum.User.ToString();
                        await _roleManager.CreateAsync(role);
                        break;
                    case "Viewer":
                        role.Name = UserTypeEnum.Viewer.ToString();
                        await _roleManager.CreateAsync(role);
                        break;
                    case "Guest":
                        role.Name = UserTypeEnum.Guest.ToString();
                        await _roleManager.CreateAsync(role);
                        break;
                }
            }
            else if (resault == true) // check if user has role IF has return else assign role
            {
                ApplicationRole role = new ApplicationRole();

                var user = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=> x.Type == "name"); 
                var dbuser = _userManager.FindByNameAsync(user.Value).Result;

                if (_httpContextAccessor.HttpContext.User.IsInRole(s))
                {
                    return "User has a Role";
                }
                await _userManager.AddToRoleAsync(dbuser,role.ToString()); // BROKEN FIX HOW TO OPTAIN USER INFO
                return "Role granted";
            }
            return "Incorrect role";
        }
    }
}