using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Entities;
using ToDo.Enums;
using ToDo.Interfaces;
using ToDo.Resources;
using ToDo.Resources.Filters;

namespace ToDo.Services
{
    public class AccountManagementService : IAccountManagementService
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTtokenCreationService _jWTtokenService;
        private readonly IRoleManagementService _userType;
        public AccountManagementService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJWTtokenCreationService jWTtokenService, IRoleManagementService userType)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTtokenService = jWTtokenService;
            _userType = userType;
        }

        // USER CREATION //
        public async Task<string> CreateUser(RegisterResource registerResource)
        {
            var user = registerResource.Adapt<ApplicationUser>();
            var resault = await _userManager.CreateAsync(user, registerResource.Password);

            if (resault.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false, authenticationMethod: null);

                var role = registerResource.Adapt<RoleValue>();// what if someone deleted the user role / this becomes invalid (??)

                await _userType.RoleAssign(role);
                return _jWTtokenService.CreateJWTtoken(user);
            }
            throw new Exception(string.Join(" / ", resault.Errors.Select(e => e.Description)));
        }

        // USER LOGIN //

        public async Task<string> Login(LoginResource dtoUsers)
        {
            var user = await _userManager.FindByNameAsync(dtoUsers.UserName);
            var resault = await _userManager.CheckPasswordAsync(user, dtoUsers.Password);
            if (resault)
            {
                return _jWTtokenService.CreateJWTtoken(user);
                // perm create/give
            }
            throw new Exception("Username or Password is Incorrect!!");
        }

        // USER LOGOUT // 

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}