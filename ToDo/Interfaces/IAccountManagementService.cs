using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Resources;
using ToDo.IdentityEntity_s;
using ToDo.Entitys;

namespace ToDo.Interfaces
{
    public interface IAccountManagementService
    {
        public Task<string> CreateUser(RegisterResource dtoUsers);
        public Task<string> Login(LoginResource dtoUsers);
        public Task Logout();
    }
}
