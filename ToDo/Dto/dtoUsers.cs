using System.ComponentModel.DataAnnotations;
using ToDo.Enums;

namespace ToDo.Dto
{
    public class dtoUsers
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        public UserType Utype { get; set; } = UserType.User;
    }
}
