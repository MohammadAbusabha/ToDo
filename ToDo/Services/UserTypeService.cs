using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly ClaimsPrincipal _user;

        public UserTypeService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _user = contextAccessor.HttpContext.User;
        }
        public async Task<string> RoleSelect(string rolename)
        {
            var resault = await _roleManager.RoleExistsAsync(rolename);

            if (resault)// check if user has role 
            {
                return await RoleAssign(rolename);
            } 
            else if (resault != true) // if role dont exist Create it
            {
                ApplicationRole role = new ApplicationRole();

                switch (rolename)
                {
                    case "Admin":
                        role.Name = UserTypeEnum.Admin.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(rolename);
                        return "Role Created!";
                    case "User":
                        role.Name = UserTypeEnum.User.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(rolename);
                        return "Role Created!";
                    case "Viewer":
                        role.Name = UserTypeEnum.Viewer.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(rolename);
                        return "Role Created!";
                    case "Guest":
                        role.Name = UserTypeEnum.Guest.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(rolename);
                        return "Role Created!";
                    case "Test":
                        role.Name = UserTypeEnum.Test.ToString();
                        await _roleManager.CreateAsync(role);
                        await RoleAssign(rolename);
                        return "Role Created!";
                }
            }
            return "Incorrect role";
        }
        public async Task<string> RoleAssign(string s)
        {
            var name = _user.Identity.Name;
            var user = _userManager.FindByNameAsync(name).Result;

            //if (_userManager.GetRolesAsync(user) != null) // change to handle errors
            //{
            //    return "User has a Role";
            //}

            await _userManager.AddToRoleAsync(user, s);
            return "Role granted";
        }
    }
}