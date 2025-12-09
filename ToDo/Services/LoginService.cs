using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IUserType _userType;
        public LoginService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJWTtoken jWTtokenService, IUserType userType)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTtokenService = jWTtokenService;
            _userType = userType;
        }
        public async Task<string> CreateUser(RegisterDTO dtoUsers)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = dtoUsers.Username,
                Email = dtoUsers.EmailAddress,
            };
            var resault = await _userManager.CreateAsync(user, dtoUsers.Password);

            if (resault.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false, authenticationMethod: null);

                RoleDTO roleDTO = new RoleDTO()
                {
                    UserName = dtoUsers.Username,
                    RoleName = "User"
                };
                await _userType.RoleAssign(roleDTO);

                var token = _jWTtokenService.CreateJWTtoken(user);
                return token;
            }
            throw new Exception(string.Join(" / ", resault.Errors.Select(e => e.Description)));
        }
        public async Task<string> Login(LoginDTO dtoUsers)
        {
            var user = await _userManager.FindByNameAsync(dtoUsers.Username);
            var resault = await _userManager.CheckPasswordAsync(user, dtoUsers.Password);
            if(resault)
            {
                var token =  _jWTtokenService.CreateJWTtoken(user);
                return token;
            }
            throw new Exception("Username or Password is Incorrect!!");
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}