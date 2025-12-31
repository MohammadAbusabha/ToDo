using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ToDo.Core.Interfaces;

namespace ToDo.Core.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal _user;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor.HttpContext.User;
        }
        // create obj of appuser
        // return user.name for example
        public Guid UserId
        {
            get
            {
                if(_user != null)
                {
                    if (Guid.TryParse(_user.FindFirstValue(ClaimTypes.NameIdentifier), out Guid result))
                    {
                        return result;
                    }
                }
                return Guid.Empty;
            }
        }
        public string Name
        {
            get
            {
                if (_user != null)
                {
                    var name = _user.FindFirstValue(ClaimTypes.Name);
                    if (name !=null)
                    {
                        return name;
                    }
                }
                return string.Empty;
            }
        }
        public string Email
        {
            get
            {
                if (_user != null)
                {
                    var email = _user.FindFirstValue(ClaimTypes.Email);
                    if (email != null)
                    {
                        return email;
                    }
                }
                return string.Empty;
            }
        }
        public List<string> RoleNames
        {
            get
            {
                if (_user != null)
                {
                    var claimRoles = _user.FindAll(ClaimsIdentity.DefaultRoleClaimType);
                    var rolenames = new List<string>();
                    foreach (var role in claimRoles)
                    {
                        rolenames.Add(role.Value);
                    }
                    return rolenames;
                }
                return new List<string>();
            }
        }
    }
}