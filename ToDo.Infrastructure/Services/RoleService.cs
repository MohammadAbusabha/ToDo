using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDo.Core.Entities;
using ToDo.Core.Resources;
using ToDo.Core.Interfaces;

namespace ToDo.Infrastructure.Services
{
    [AllowAnonymous]
    public class RoleService : IRoleService // mostly wrong need to go over
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        //public async Task<string> CreateRoleAsync(int value)
        //{
        //    // checks if role is valid
        //    string rolename = Enum.GetName(typeof(RoleLevel), value);
        //    if (rolename == null)
        //    {
        //        throw new Exception("Role is not valid!");
        //    }

        //    // creates the role if it does not exist
        //    var resault = _roleManager.RoleExistsAsync(rolename).Result;
        //    if (!resault)
        //    {
        //        //var role = rolename.Adapt<ApplicationRole>(); 
        //        // mapster breaks since rolename is a string 
        //        //still dont know if this is best practice here or not
        //        ApplicationRole role = new()
        //        {
        //            Name = rolename,
        //            Value = value
        //        };
        //        await _roleManager.CreateAsync(role);
        //    }
        //    return rolename;
        //}
        public async Task<string> RoleAssignAsync(RoleResource roleResource)
        {
            //var role = await CreateRoleAsync(filter.Value);
            var user = _userManager.FindByNameAsync(roleResource.UserName).Result;
            //var userId = _user.Id.ToString();
            //var user = _userManager.FindByIdAsync(userId).Result;
            await _userManager.AddToRoleAsync(user, roleResource.RoleName);
            return "Role granted";
        }
    }
}