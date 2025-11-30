using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public AccountController(ILogin ilogin)
        {
            _ilogin = ilogin;
        }

        [HttpPost]
        public async Task PostRegister(dtoUsers dtoUsers)
        {
              await _ilogin.Postregister(dtoUsers);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(dtoUsers dtoUsers)
        {
            if (await _ilogin.LogIn(dtoUsers) == true)
            {
                return Ok(dtoUsers.UserName);
            }
            else return BadRequest();
        }
        [HttpGet("SignOut")]
        public async Task SignOut()
        {
            await _ilogin.SignOut();
        }
    }
}