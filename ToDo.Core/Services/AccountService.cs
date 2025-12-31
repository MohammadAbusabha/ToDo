using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Core.Services
{
    public class AccountService : IAccountService
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTService _jWTtokenService;
        public AccountService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJWTService jWTtokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTtokenService = jWTtokenService;
        }

        // USER CREATION //
        public async Task<string> CreateUser(RegisterResource registerResource) 
        {
            var user = registerResource.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, registerResource.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Guest");
                return "User Created";
            }
            throw new Exception(string.Join(" / ", result.Errors.Select(e => e.Description)));
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
            throw new Exception("Username or Password is Incorrect!!");
        }

        // USER LOGOUT // 

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}