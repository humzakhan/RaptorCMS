namespace Raptor.Services.Logging
{
    public class ActivityLogDefaults
    {
        /// <summary>
        /// Login Activity 
        /// </summary>
        public static readonly string LoggedIn = "user.login";

        /// <summary>
        /// User logout
        /// </summary>
        public static readonly string LoggedOut = "user.logout";

        /// <summary>
        /// Update Profie Activity
        /// </summary>
        public static readonly string UpdateProfile = "user.update.profile";

        /// <summary>
        /// Update Password Activity
        /// </summary>
        public static readonly string UpdatePassword = "user.update.password";

        /// <summary>
        /// Add blog post category
        /// </summary>
        public static readonly string AddBlogPostCategory = "blog.category.add";

        /// <summary>
        /// View blog post categories
        /// </summary>
        public static readonly string ViewBlogPostCategories = "blog.category.view";

        /// <summary>
        /// Edit a blog post category
        /// </summary>
        public static readonly string EditBlogPostCategory = "blog.category.edit";

        /// <summary>
        /// Add blog post
        /// </summary>
        public static readonly string AddBlogPost = "blog.post.add";

        /// <summary>
        /// Update blog post
        /// </summary>
        public static readonly string UpdateBlogPost = "blog.post.edit";

        /// <summary>
        /// View logs
        /// </summary>
        public static readonly string ViewLogs = "logs.view";

        /// <summary>
        /// Create user roles
        /// </summary>
        public static readonly string CreateRole = "roles.create";

        /// <summary>
        /// View user roles
        /// </summary>
        public static readonly string ViewRoles = "roles.view";

        /// <summary>
        /// Edit user roles
        /// </summary>
        public static readonly string EditRoles = "roles.edit";

        /// <summary>
        /// Delete user roles
        /// </summary>
        public static readonly string DeleteRoles = "roles.delete";

        /// <summary>
        /// Update settings
        /// </summary>
        public static readonly string UpdateSettings = "settings.edit";
    }
}
