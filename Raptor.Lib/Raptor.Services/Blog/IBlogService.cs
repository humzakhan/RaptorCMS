using Raptor.Data.Models.Blog;
using System.Collections.Generic;

namespace Raptor.Services.Blog
{
    public interface IBlogService
    {
        #region Blog Posts

        BlogPost GetBlogPostById(int blogPostId);
        IList<BlogPost> GetBlogPostByIds(int[] blogPostIds);
        void DeleteBlogPost(BlogPost blogPost);
        void CreateBlogPost(BlogPost blogPost);
        void UpdateBlogPost(BlogPost blogPost);
        IList<BlogPostCategory> GetBlogPostCategories();

        #endregion

        #region Blog Comments

        IList<BlogComment> GetBlogCommentsById(int blogPostId);
        IList<BlogComment> GetBlogCommentsByIds(int[] blogPostIds);
        int GetBlogCommentsCount(int blogPostId, bool isApproved = true);
        int CountEntities(BlogEntityType entity);
        void DeleteBlogComment(BlogComment comment);
        void DeleteBlogComments(IList<BlogComment> comments);

        #endregion

        #region Blog Post Categories

        void CreateBlogPostCategory(BlogPostCategory category);
        void CreateBlogPostCategory(string name, string slug, string description);
        void UpdateBlogPostCategory(BlogPostCategory category);

        #endregion
    }

    public enum BlogEntityType
    {
        Posts,
        Comments
    }
}
