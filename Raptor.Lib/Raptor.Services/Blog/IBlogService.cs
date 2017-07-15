using Raptor.Data.Models;
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

        #endregion

        #region Blog Comments

        IList<Comment> GetBlogCommentsById(int blogPostId);
        IList<Comment> GetBlogCommentsByIds(int[] blogPostIds);
        int GetBlogCommentsCount(int blogPostId, bool isApproved = true);
        void DeleteBlogComment(Comment comment);
        void DeleteBlogComments(IList<Comment> comments);

        #endregion
    }
}
