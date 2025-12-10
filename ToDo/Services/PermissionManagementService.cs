using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Context;
using ToDo.Enums;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Resources;

namespace ToDo.Services
{
    public class PermissionManagementService : IPermissionManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public PermissionManagementService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Claim> CheckPermission(string permission)
        {
            var permissionNames = typeof(Permission).GetEnumNames();
            foreach (var name in permissionNames)
            {
                if (name == permission)
                {
                    return new Claim("Permission", permission);
                }
            }
            throw new Exception("Wrong Permission Name");
        }
        public async Task Addpermission(PermissionResource permissionResource)
        {
            var claim = await CheckPermission(permissionResource.Permission);
            var user = await _userManager.FindByNameAsync(permissionResource.Username);
            await _userManager.AddClaimAsync(user, claim);
        }
    }
}
