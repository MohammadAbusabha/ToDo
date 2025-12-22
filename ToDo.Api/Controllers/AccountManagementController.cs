using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Resources;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private readonly IAccountManagementService _ilogin;
        private readonly IRoleManagementService _type;

        public AccountManagementController(IAccountManagementService ilogin, IRoleManagementService type)
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
        public Task<string> RoleAssign(RoleValue role)
        {
            return _type.RoleAssign(role);
        }

    }
}