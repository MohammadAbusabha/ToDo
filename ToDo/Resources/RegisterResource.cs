using System.ComponentModel.DataAnnotations;

namespace ToDo.Resources
{
    public class RegisterResource : LoginResource
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
    }
    public class LoginResource
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
