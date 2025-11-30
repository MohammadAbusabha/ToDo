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

        public LoginService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task Postregister(dtoUsers dtoUsers)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = dtoUsers.UserName,
                Email = dtoUsers.EmailAddress,
            };
            await _userManager.CreateAsync(user, dtoUsers.Password);
        }
        public async Task<bool> LogIn(dtoUsers dtoUsers)
        {
            var result = await _signInManager.PasswordSignInAsync(dtoUsers.UserName, dtoUsers.Password, false, lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return true;
            }
            else return false;
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}