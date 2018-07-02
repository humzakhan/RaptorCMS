namespace Raptor.Data.Models.Configuration
{
    public class SettingValues
    {
        /// <summary>
        /// Site name
        /// </summary>
        public string SiteName;

        /// <summary>
        /// Site description
        /// </summary>
        public string SiteDescription;

        /// <summary>
        /// RaptorCMS verison
        /// </summary>
        public string RaptorVersion;

        /// <summary>
        /// Registrations enabled for new members
        /// </summary>
        public bool IsRegistrationEnabled;

        /// <summary>
        /// Comments enabled for blog posts
        /// </summary>
        public bool IsCommentsEnabled;

        /// <summary>
        /// Facebook url
        /// </summary>
        public string FacebookUrl;

        /// <summary>
        /// Twitter url
        /// </summary>
        public string TwitterUrl;

        /// <summary>
        /// Instagram url
        /// </summary>
        public string InstagramUrl;

        /// <summary>
        /// Youtube Url
        /// </summary>
        public string YoutubeUrl;

        /// <summary>
        /// Default user role for newly registered users
        /// </summary>
        public int DefaultUserRoleId;
    }
}
