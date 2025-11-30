using System.ComponentModel.DataAnnotations;

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
    }
}
