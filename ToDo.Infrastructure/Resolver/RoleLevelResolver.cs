using ToDo.Core.Interfaces;
using ToDo.Infrastructure.Context;

namespace ToDo.Infrastructure.Resolver
{
    public class RoleLevelResolver : IRoleLevelResolver
    {
        private readonly DataContext _context;
        public RoleLevelResolver(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<int> RoleLevels(IEnumerable<string> roleNames)
        {
            var levels = new List<int>();
            foreach (var name in roleNames)
            {
                levels.Add(_context.Roles
                    .Where(x => x.Name == name)
                    .Select(x => x.Value)
                    .First());
            }
            return levels;
        }
    }
}