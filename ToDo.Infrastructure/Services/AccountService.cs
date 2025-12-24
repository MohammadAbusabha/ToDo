using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;
using ToDo.Core.Enums;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Infrastructure.Services
{
    public class AccountService : IAccountService
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJWTService _jWTtokenService;
        private readonly IRoleService _role;
        public AccountService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJWTService jWTtokenService, IRoleService role, RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTtokenService = jWTtokenService;
            _role = role;
            _roleManager = roleManager;
        }

        // USER CREATION //
        public async Task<string> CreateUser(RegisterResource registerResource) // should be good
        {
            var user = registerResource.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, registerResource.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleLevel.Viewer.ToString());
                return "User Created";
            }
            throw new Exception(string.Join(" / ", result.Errors.Select(e => e.Description)));// exception handling should be done else where 
        }

        // USER LOGIN //

        public async Task<string> Login(LoginResource dtoUsers)
        {
            var user = await _userManager.FindByNameAsync(dtoUsers.UserName);
            var result = await _userManager.CheckPasswordAsync(user, dtoUsers.Password);
            if (result)
            {
                return _jWTtokenService.CreateJWTtoken(user);
            }
            throw new Exception("Username or Password is Incorrect!!");// exception handling should be done else where
        }

        // USER LOGOUT // 

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}