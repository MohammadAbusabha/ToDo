using ToDo.Dto;
using ToDo.IdentityEntity_s;

namespace ToDo.Interfaces
{
    public interface IJWTtoken
    {
        string CreateJWTtoken(ApplicationUser user);
    }
}
