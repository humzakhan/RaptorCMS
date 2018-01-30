using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Repeat Password")]
        [Compare(otherProperty: nameof(Password), ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }

        public string Link { get; set; }
    }
}
