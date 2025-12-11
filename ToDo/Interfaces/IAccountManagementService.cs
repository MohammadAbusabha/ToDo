using System.Threading.Tasks;
using ToDo.Resources;

namespace ToDo.Interfaces
{
    public interface IAccountManagementService
    {
        public Task<string> CreateUser(RegisterResource dtoUsers);
        public Task<string> Login(LoginResource dtoUsers);
        public Task Logout();
    }
}
