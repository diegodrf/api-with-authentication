using System.ComponentModel.DataAnnotations;

namespace ApiWithAuthentication.Models.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
