using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using System.Security.Claims;
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
        }
        public Guid Id
        {
            get
            {

            }
        }
        public string Name
        {
            get
            {
                return _user.FindFirstValue(ClaimTypes.Name);
            }
        }
        public string Email
        {
            get
            {
                return _user.FindFirstValue(ClaimTypes.Email);
            }
        }
        public List<string> Roles // should not get roles from claims here
        {
            get
            {
                var currRoles =_userManager
                    .GetRolesAsync(_userManager.FindByIdAsync(Id.ToString()).Result)
                    .Result;
                List<string> roles = new List<string>();
                foreach (var role in currRoles)
                {
                    roles.Add(role.ToString());
                }
                return roles;
            }
        }
        public int TopRole
        {
            get
            {
                return 0;
            }
        }
    }
}