using ToDo.Resources;
using ToDo.IdentityEntity_s;

namespace ToDo.Interfaces
{
    public interface IJWTtokenCreationService
    {
        string CreateJWTtoken(ApplicationUser user);
    }
}
