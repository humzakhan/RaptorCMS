using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
