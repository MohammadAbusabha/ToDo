using System.Threading.Tasks;
using ToDo.Dto;
using ToDo.IdentityEntity_s;

namespace ToDo.Interfaces
{
    public interface IUserType
    {
        public Task<string> RoleSelect(string s);
    }
}
