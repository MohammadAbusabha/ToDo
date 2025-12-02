using Microsoft.AspNetCore.Mvc;
using ToDo.Dto;
using ToDo.IdentityEntity_s;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ILogin
    {
        public Task PostRegister(dtoUsers dtoUsers, bool userOrAdmin);
        public Task<bool> LogIn(dtoUsers dtoUsers);
        public Task SignOut();
    }
}
