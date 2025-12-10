using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using ToDo.Resources;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Entitys;

namespace ToDo.Controllers
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
        public async Task<string> CreateUser(RegisterResource dtoUsers)
        {
            return await _ilogin.CreateUser(dtoUsers);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<string> Login(LoginResource dtoUsers)
        {
            return await _ilogin.Login(dtoUsers);
        }
        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task Logout()
        {
            await _ilogin.Logout();
        }

        [HttpPut("Role Selection")]
        public Task<string> RoleAssign(RoleResource roleDTO)
        {
            return _type.RoleAssign(roleDTO);
        }

    }
}