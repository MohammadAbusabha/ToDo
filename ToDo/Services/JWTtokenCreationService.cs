using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ToDo.Entities;
using ToDo.Enums;
using ToDo.Interfaces;

namespace ToDo.Services
{
    public class JWTtokenCreationService : IJWTtokenCreationService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _user;
        private readonly IPrivilegeManagementService _privilege;
        public JWTtokenCreationService
            (IConfiguration configuration, ICurrentUserService currentUser, IPrivilegeManagementService privilegeManagementService)
        {
            _configuration = configuration;
            _user = currentUser;
            _privilege = privilegeManagementService;
        }

        public string CreateJWTtoken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])); // Security key //
            var signCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Credentials //
            var expire = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["expiration_minutes"])); // Token lifetime // 

            // user claims //

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            // Role Claim //

            var roles = _user.CurrentUserRoles(user).Result;
            foreach (var role in roles)
            {
                int value = (int)Enum.Parse<Role>(role);
                claims.Add(new Claim("Role", value.ToString()));
                //claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Permissions Claims //

            int maxValue = claims.Where(c=>c.Type == "Role").Select(c=>int.Parse(c.Value)).Max();
            var privilegeList = new List<int>();
            for(int privilegeValue = 0; privilegeValue <= maxValue; privilegeValue++)
            {
                privilegeList.Add(privilegeValue);
            }
            var privilegeClaims = _privilege.CreatePrivilege(privilegeList).Result;

            foreach(var privilege in privilegeClaims)
            {
                claims.Add(privilege);
            }

            // Token //

            var token = new JwtSecurityToken(issuer: "http://localhost:5298", audience: "MyApi", claims, expires: expire, signingCredentials: signCred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
