using ToDo.Core.Enums;

namespace ToDo.Core.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
        public string Name { get; }
        public string Email { get; }
        public List<string> RoleNames { get; }
    }
}
