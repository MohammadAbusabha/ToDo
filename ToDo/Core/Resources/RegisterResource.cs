using System.ComponentModel.DataAnnotations;

namespace ToDo.Core.Resources
{
    public class RegisterResource : LoginResource
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
    public class LoginResource
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
