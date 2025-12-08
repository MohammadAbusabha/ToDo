using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using ToDo.Dto;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogin _ilogin;
        private readonly IUserType _type;

        public AccountController(ILogin ilogin, IUserType type)
        {
            _ilogin = ilogin;
            _type = type;
        }

        [HttpPost("CreateAccount")]
        public async Task<string> CreateUser(RegisterDTO dtoUsers)
        {
            return await _ilogin.CreateUser(dtoUsers);
        }

        [HttpPost("Login")]
        public async Task<string> Login(LoginDTO dtoUsers)
        {
            return await _ilogin.Login(dtoUsers);
        }

        [HttpPost("Logout")]
        public async Task Logout()
        {
            await _ilogin.Logout();
        }

        [HttpPut("Role Selection")]
        public Task<string> RoleSelect(string roleName)
        {
            return _type.RoleSelect(roleName);
        }

    }
}