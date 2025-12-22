using Microsoft.AspNetCore.Identity;

namespace ToDo.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Data> Data { get; set; }
    }
}
