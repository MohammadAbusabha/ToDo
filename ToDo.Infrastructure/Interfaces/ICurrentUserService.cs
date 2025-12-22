using ToDo.Core.Entities;

namespace ToDo.Infrastructure.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public List<string> Roles { get; }
        public int TopRole { get; }
    }
}
