using ToDo.Core.Entities;

namespace ToDo.Core.Interfaces
{
    public interface IJWTService
    {
        string CreateJWTtoken(ApplicationUser user);
    }
}
