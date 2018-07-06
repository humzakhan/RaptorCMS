using Raptor.Data.Models.Blog;
using System.Collections.Generic;

namespace Raptor.Services.Blog
{
    public interface IBlogService
    {
        #region Blog Posts

        BlogPost GetBlogPostById(int blogPostId);
        BlogPost GetBlogPostByLink(string link);
        IList<BlogPost> GetBlogPostByIds(int[] blogPostIds);
        void DeleteBlogPost(BlogPost blogPost);
        void CreateBlogPost(BlogPost blogPost);
        void UpdateBlogPost(BlogPost blogPost);
        IEnumerable<BlogPost> GetBlogPosts(int categoryId = 0, int mostRecentCount = 0);

        #endregion

        #region Blog Comments

        IList<BlogComment> GetBlogCommentsById(int blogPostId);
        IList<BlogComment> GetBlogCommentsByIds(int[] blogPostIds);
        IEnumerable<BlogComment> GetAllBlogComments();
        int GetBlogCommentsCount(int blogPostId, bool isApproved = true);
        int CountEntities(BlogEntityType entity);
        void DeleteBlogComment(BlogComment comment);
        void DeleteBlogComments(IList<BlogComment> comments);

        #endregion

        #region Blog Post Categories

        void CreateBlogPostCategory(BlogPostCategory category);
        void CreateBlogPostCategory(string name, string slug, string description);
        void UpdateBlogPostCategory(BlogPostCategory category);
        BlogPostCategory GetBlogPostCategoryById(int id);
        BlogPostCategory GetBlogPostCategoryBySlug(string slug);
        IList<BlogPostCategory> GetBlogPostCategories(bool includePosts = false);
        #endregion
    }

    public enum BlogEntityType
    {
        Posts,
        Comments
    }
}
