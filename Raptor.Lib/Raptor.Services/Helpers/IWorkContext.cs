using Raptor.Data.Models.Configuration;
using Raptor.Data.Models.Users;

namespace Raptor.Services.Helpers
{
    public interface IWorkContext
    {
        /// <summary>
        /// Current user object
        /// </summary>
        Person CurrentUser { get; set; }

        /// <summary>
        /// Whether the current user is admin or not
        /// </summary>
        bool IsAdmin { get; set; }

        /// <summary>
        /// Global settings for the web app
        /// </summary>
        SettingValues Settings { get; set; }
    }
}
