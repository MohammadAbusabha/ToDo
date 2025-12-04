using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDo.Dto;
using ToDo.IdentityEntity_s;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ILogin
    {
        public Task<string> Register(RegisterDTO dtoUsers);
        public Task<string> Login(LoginDTO dtoUsers);
        public Task Signout();
    }
}
