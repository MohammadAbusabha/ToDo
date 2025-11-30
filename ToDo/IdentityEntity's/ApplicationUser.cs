using Microsoft.AspNetCore.Identity;
using ToDo.Models;

namespace ToDo.IdentityEntity_s
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //public Data Data { get; set; }
        public ICollection<Data> datas { get; set; }
    }
}
