using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Resources;

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
        public async Task<string> CreateUser(RegisterResource dtoUsers)
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

                RoleResource roleDTO = new RoleResource()
                {
                    UserName = dtoUsers.Username,
                    RoleName = "Viewer"
                };
                await _userType.RoleAssign(roleDTO);

                var token = _jWTtokenService.CreateJWTtoken(user);
                return token;
            }
            throw new Exception(string.Join(" / ", resault.Errors.Select(e => e.Description)));
        }

        // USER LOGIN //

        public async Task<string> Login(LoginResource dtoUsers)
        {
            var user = await _userManager.FindByNameAsync(dtoUsers.Username);
            var resault = await _userManager.CheckPasswordAsync(user, dtoUsers.Password);
            if (resault)
            {
                var token = _jWTtokenService.CreateJWTtoken(user);
                return token;
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