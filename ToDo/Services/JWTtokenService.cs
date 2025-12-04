using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Dto;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using static System.Net.WebRequestMethods;

namespace ToDo.Services
{
    public class JWTtokenService : IJWTtoken
    {
        private readonly IConfiguration _configuration;

        public JWTtokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateJWTtoken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var signCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["expiration_minutes"]));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,  user.UserName),
            };

            var token = new JwtSecurityToken(issuer: "http://localhost:5298", audience: "MyApi", claims, expires:expire, signingCredentials:signCred );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
