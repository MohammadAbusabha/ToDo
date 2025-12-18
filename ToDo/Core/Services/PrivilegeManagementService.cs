using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Enums;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Core.Services
{
    public class PrivilegeManagementService : IPrivilegeManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _datacontext;
        public PrivilegeManagementService(UserManager<ApplicationUser> userManager, DataContext dataContext)
        {
            _userManager = userManager;
            _datacontext = dataContext;
        }
        public async Task<List<Claim>> CreatePrivilege(List<int> values)
        {
            var claims = new List<Claim>();

            foreach (var value in values)
            {
                string perName = Enum.GetName(typeof(Privileges), value);
                claims.Add(new Claim("Privilege", perName));
            }
            return claims;
        }
        //dont delete just yet (might use later) for perms

        //public async Task AddPrivilege(ApplicationUser appuser)
        //{
        //    var currentUserRoles = _userManager.GetRolesAsync(appuser).Result;

        //}
    }
}
