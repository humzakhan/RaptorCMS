using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public int BusinessEntityId { get; set; }

        public string Title { get; set; }

        public string Action { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(180)]
        public string DisplayName { get; set; }

        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string About { get; set; }
        public string Website { get; set; }

        [Display(Name = "Is Blocked?")]
        public bool IsBlocked { get; set; }

        [Display(Name = "Is Email Verified?")]
        public bool IsEmailVerified { get; set; }

        [Display(Name = "Is Deleted?")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Avatar { get; set;  }

        [Display(Name = "Repeat Password")]
        [Compare(otherProperty: nameof(Password), ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }

        [Display(Name = "Facebook Page Url")]
        public string FacebookUrl { get; set; }

        [Display(Name = "Twitter Handle")]
        public string TwitterUrl { get; set; }

        [Display(Name = "Instagtam Page Url")]
        public string InstagramUrl { get; set; }

        [Display(Name = "Youtube Page Url")]
        public string YoutubeUrl { get; set; }
    }
}
