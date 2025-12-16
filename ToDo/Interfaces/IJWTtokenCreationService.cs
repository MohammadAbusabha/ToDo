using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IJWTtokenCreationService
    {
        string CreateJWTtoken(ApplicationUser user);
    }
}
