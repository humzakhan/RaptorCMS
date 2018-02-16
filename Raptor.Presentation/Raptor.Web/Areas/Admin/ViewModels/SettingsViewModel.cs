using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class SettingsViewModel
    {
        [Required]
        [Display(Name = "Site Name")]
        public string SiteName { get; set; }

        [Display(Name = "Site Description")]
        public string SiteDescription { get; set; }

        [Display(Name = "Allow new registerations?")]
        public bool IsRegistrationEnabled { get; set; }

        [Display(Name = "Allow Comments?")]
        public bool IsCommentsEnabled { get; set; }

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
