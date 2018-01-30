namespace Raptor.Services.Logging
{
    public class ActivityLogDefaults
    {
        /// <summary>
        /// Login Activity 
        /// </summary>
        public static readonly string Login = "user.login";

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
        public static readonly string AddBlogPostCategory = "user.blog.category.add";

        /// <summary>
        /// View blog post categories
        /// </summary>
        public static readonly string ViewBlogPostCategories = "user.blog.category.view";

        /// <summary>
        /// Edit a blog post category
        /// </summary>
        public static readonly string EditBlogPostCategory = "user.blog.category.edit";
    }
}
