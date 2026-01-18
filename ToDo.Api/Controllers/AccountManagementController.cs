using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private readonly IAccountService _ilogin;
        private readonly IRoleService _role;

        public AccountManagementController(IAccountService ilogin, IRoleService type)
        {
            _ilogin = ilogin;
            _role = type;
        }

        [AllowAnonymous]
        [HttpPost("CreateAccount")]
        public async Task<string> CreateUser(RegisterResource user)
        {
            return await _ilogin.CreateUser(user);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<string> Login(LoginResource user)
        {
            return await _ilogin.Login(user);
        }
        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task Logout()
        {
            await _ilogin.Logout();
        }

        [HttpPut("Create Role")]
        public async Task CreateRole(RolePrivilegeResource rolePrivilegeResource)
        {
            await _role.CreateRoleWithPrivilegeAsync(rolePrivilegeResource);
        }
        [HttpPut("Grant Role")]
        public async Task AddToRole(RoleResource roleResource)
        {
            await _role.AddToRoleAsync(roleResource);
        }

    }
}