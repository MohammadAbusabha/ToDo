using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Dto;
using ToDo.Enums;
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
        private readonly RoleManager<ApplicationRole> _roleManager;

        public LoginService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task PostRegister(dtoUsers dtoUsers, bool userOrAdmin)
        {
            ApplicationRole role = new ApplicationRole();
            ApplicationUser user = new ApplicationUser()
            {
                UserName = dtoUsers.UserName,
                Email = dtoUsers.EmailAddress,
            };
            await _userManager.CreateAsync(user, dtoUsers.Password);

            if (userOrAdmin == true)
            {
                role.Name = UserType.Admin.ToString();
                await _roleManager.CreateAsync(role);
            }
            else
            {
                role.Name = UserType.User.ToString();
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, role.ToString());

        }
        public async Task<bool> LogIn(dtoUsers dtoUsers)
        {
            var result = await _signInManager.PasswordSignInAsync(dtoUsers.UserName, dtoUsers.Password, isPersistent:true, lockoutOnFailure:false);
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