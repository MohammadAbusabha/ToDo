using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Interfaces
{
    public interface IJWTtokenCreationService
    {
        string CreateJWTtoken(ApplicationUser user);
    }
}
