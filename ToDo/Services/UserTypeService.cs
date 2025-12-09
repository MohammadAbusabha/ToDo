using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using Microsoft.VisualBasic;
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
    [AllowAnonymous]
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
        public async Task<string> CreateRole(string rolename)
        {
            var resault = await _roleManager.RoleExistsAsync(rolename);

            if (!resault)
            {
                ApplicationRole role = new ApplicationRole();

                switch (rolename)
                {
                    case "Admin":
                        role.Name = UserTypeEnum.Admin.ToString();
                        await _roleManager.CreateAsync(role);
                        return "Role Created!";
                    case "User":
                        role.Name = UserTypeEnum.User.ToString();
                        await _roleManager.CreateAsync(role);
                        return "Role Created!";
                    case "Viewer":
                        role.Name = UserTypeEnum.Viewer.ToString();
                        await _roleManager.CreateAsync(role);
                        return "Role Created!";
                    case "Guest":
                        role.Name = UserTypeEnum.Guest.ToString();
                        await _roleManager.CreateAsync(role);
                        return "Role Created!";
                    case "Test":
                        role.Name = UserTypeEnum.Test.ToString();
                        await _roleManager.CreateAsync(role);
                        return "Role Created!";
                }
            }
            return "Role Exist"; 
        }
        public async Task<string> RoleAssign(RoleDTO roleDTO)
        {
            await CreateRole(roleDTO.RoleName);

            var username = _user.Identity.Name;

            var user = _userManager.FindByNameAsync(roleDTO.UserName).Result;
            await _userManager.AddToRoleAsync(user, roleDTO.RoleName);
            return "Role granted";
        }
    }
}