using System.ComponentModel.DataAnnotations;

namespace ToDo.Dto
{
    public class RegisterDTO : LoginDTO
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
    }
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
