using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(50)]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Repeat Email Address")]
        [StringLength(50)]
        [Required]
        [Compare(nameof(EmailAddress), ErrorMessage = "Email Addresses do not match.")]
        [EmailAddress]
        public string RepeatEmailAddress { get; set; }

        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Repeat Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string RepeatPassword { get; set; }

    }
}
