using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username or Email Address")]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
