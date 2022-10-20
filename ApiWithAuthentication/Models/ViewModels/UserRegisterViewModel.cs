using System.ComponentModel.DataAnnotations;

namespace ApiWithAuthentication.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
