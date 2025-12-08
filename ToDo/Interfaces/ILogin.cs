using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Dto;
using ToDo.IdentityEntity_s;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ILogin
    {
        public Task<string> CreateUser(RegisterDTO dtoUsers);
        public Task<string> Login(LoginDTO dtoUsers);
        public Task Logout();
    }
}
