using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Enums;
using ToDo.Entities;
using ToDo.Interfaces;
using ToDo.Resources;
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using System.Collections.Generic;

namespace ToDo.Services
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
                string perName = Enum.GetName(typeof(Enums.Privileges), value);
                claims.Add(new Claim("Privilege", perName));
            }
            return claims;
        }
        //public async Task AddPrivilege(ApplicationUser appuser)
        //{
        //    var currentUserRoles = _userManager.GetRolesAsync(appuser).Result;

        //}
    }
}
