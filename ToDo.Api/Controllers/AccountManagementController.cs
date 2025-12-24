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
        private readonly IRoleService _type;

        public AccountManagementController(IAccountService ilogin, IRoleService type)
        {
            _ilogin = ilogin;
            _type = type;
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

        [HttpPut("Role Selection")]
        public Task<string> RoleAssign(RoleResource roleResource)
        {
            return _type.RoleAssignAsync(roleResource);
        }

    }
}