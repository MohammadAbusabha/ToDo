using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal _user;
        private readonly UserManager<ApplicationUser> _userManager;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _user = httpContextAccessor.HttpContext.User;
            _userManager = userManager;
        }
        public Guid CurrentUserId()
        {
            return Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public string CurrentUserName()
        {
            return _user.FindFirstValue(ClaimTypes.Name);
        }
        public string CurrentUserEmail()
        {
            return _user.FindFirstValue(ClaimTypes.Email);
        }
        public async Task<List<string>> CurrentUserRoles(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user).Result.ToList();
        }
    }
}