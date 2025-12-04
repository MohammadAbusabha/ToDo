using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Dto;
using ToDo.Extensions;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Services
{
    public class LoginService : ILogin
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTtoken _jWTtokenService;
        public LoginService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJWTtoken jWTtokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTtokenService = jWTtokenService;
        }
        public async Task<string> Register(RegisterDTO dtoUsers)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = dtoUsers.UserName,
                Email = dtoUsers.EmailAddress,
            };

            var resault = await _userManager.CreateAsync(user, dtoUsers.Password);

            if (resault.Succeeded)
            {
                return "User Created";
            }
            return string.Join(" / ", resault.Errors.Select(e => e.Description));
        }
        public async Task<string> Login(LoginDTO dtoUsers)
        {
            var u = await _userManager.FindByNameAsync(dtoUsers.Username);
            var resault = await _userManager.CheckPasswordAsync(u, dtoUsers.Password);
            if(resault == true)
            {
                var token =  _jWTtokenService.CreateJWTtoken(u);
                return token;
            }
            return "Username or Password is Incorrect!!";
        }
        public async Task Signout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}