using Microsoft.AspNetCore.Mvc;
using ToDo.Dto;
using ToDo.IdentityEntity_s;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ILogin
    {
        //public Task<ActionResult<dtoUsers>> PostRegister(CreateUserDto createUserDto);
        public Task Postregister(dtoUsers dtoUsers);
        public Task<bool> LogIn(dtoUsers dtoUsers);
        public Task SignOut();
    }
}
