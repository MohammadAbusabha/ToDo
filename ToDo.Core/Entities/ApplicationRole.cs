using Microsoft.AspNetCore.Identity;

namespace ToDo.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public int Value { get; set; }
    }
}
