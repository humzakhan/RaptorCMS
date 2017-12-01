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
        void DeleteBlogComment(BlogComment comment);
        void DeleteBlogComments(IList<BlogComment> comments);

        #endregion
    }
}
