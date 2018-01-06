using Raptor.Data.Models.Logging;
using System.Collections.Generic;

namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class DashboardViewModel
    {
        /// <summary>
        /// Number of posts published
        /// </summary>
        public int PostsCount { get; set; }

        /// <summary>
        /// Number of users registered
        /// </summary>
        public int UsersCount { get; set; }

        /// <summary>
        /// Number of comments published
        /// </summary>
        public int CommentsCount { get; set; }

        /// <summary>
        /// Recent logs
        /// </summary>
        public List<Log> RecentLogs { get; set; }
    }
}
