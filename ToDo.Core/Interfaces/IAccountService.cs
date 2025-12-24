using ToDo.Core.Resources;

namespace ToDo.Core.Interfaces
{
    public interface IAccountService
    {
        public Task<string> CreateUser(RegisterResource dtoUsers);
        public Task<string> Login(LoginResource dtoUsers);
        public Task Logout();
    }
}
