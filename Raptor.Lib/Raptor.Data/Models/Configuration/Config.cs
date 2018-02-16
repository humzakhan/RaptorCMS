namespace Raptor.Data.Models.Configuration
{
    public static class Config
    {
        /// <summary>
        /// Name of the site
        /// </summary>
        public static string SiteName { get; set; }

        /// <summary>
        /// Description of the site
        /// </summary>
        public static string SiteDescription { get; set; }

        /// <summary>
        /// RaptorCMS version
        /// </summary>
        public static string RaptorVersion { get; set; }

        /// <summary>
        /// Allow new members to register or not
        /// </summary>
        public static bool IsRegistrationEnabled { get; set; }

        /// <summary>
        /// Allow comments on blog posts
        /// </summary>
        public static bool IsCommentsEnabled { get; set; }

        /// <summary>
        /// Url of facebook page
        /// </summary>
        public static string FacebookUrl { get; set; }


        /// <summary>
        /// Url of twitter profile
        /// </summary>
        public static string TwitterUrl { get; set; }

        /// <summary>
        /// Url of instagram profile
        /// </summary>
        public static string InstagramUrl { get; set; }


        /// <summary>
        /// Url of Youtube Channel
        /// </summary>
        public static string YoutubeUrl { get; set; }
    }
}
