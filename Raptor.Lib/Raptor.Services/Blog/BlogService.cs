using Raptor.Data.Core;
using Raptor.Data.Models.Blog;
using System.Collections.Generic;
using System.Linq;

namespace Raptor.Services.Blog
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<BlogPost> _blogPostsRepository;
        private readonly IRepository<BlogComment> _blogCommentsRepository;
        private readonly IRepository<BlogPostCategory> _blogPostCategoriesRepository;

        public BlogService(IRepository<BlogPost> blogPostsRepository, IRepository<BlogComment> blogCommentsRepository, IRepository<BlogPostCategory> blogPostCategoiresRepository) {
            _blogPostsRepository = blogPostsRepository;
            _blogCommentsRepository = blogCommentsRepository;
            _blogPostCategoriesRepository = blogPostCategoiresRepository;
        }

        public BlogPost GetBlogPostById(int blogPostId) {
            if (blogPostId == 0)
                return null;

            return _blogPostsRepository.GetById(blogPostId);
        }

        public IList<BlogPost> GetBlogPostByIds(int[] blogPostIds) {
            return _blogPostsRepository.Find(b => blogPostIds.Contains(b.BlogPostId)).ToList();
        }

        public void DeleteBlogPost(BlogPost blogPost) {
            _blogPostsRepository.Delete(blogPost);
        }

        public void CreateBlogPost(BlogPost blogPost) {
            _blogPostsRepository.Create(blogPost);
        }

        public void UpdateBlogPost(BlogPost blogPost) {
            _blogPostsRepository.Update(blogPost);
        }

        public IList<BlogComment> GetBlogCommentsById(int blogPostId) {
            return _blogCommentsRepository.Find(c => c.PostId == blogPostId).ToList();
        }

        public IList<BlogComment> GetBlogCommentsByIds(int[] blogPostIds) {
            return _blogCommentsRepository.Find(c => blogPostIds.Contains(c.PostId)).ToList();
        }

        public int GetBlogCommentsCount(int blogPostId, bool isApproved = true) {
            return _blogCommentsRepository.Find(c => c.PostId == blogPostId && c.Approved == isApproved).Count();
        }

        public void DeleteBlogComment(BlogComment comment) {
            _blogCommentsRepository.Delete(comment);
        }

        public void DeleteBlogComments(IList<BlogComment> comments) {
            _blogCommentsRepository.DeleteRange(comments);
        }

        public IList<BlogPostCategory> GetBlogPostCategories() {
            return _blogPostCategoriesRepository.GetAll().ToList();
        }

        public int CountEntities(BlogEntityType entity) {
            var entityCount = 0;

            switch (entity) {
                case BlogEntityType.Posts:
                    entityCount = _blogPostsRepository.GetAll().Count();
                    break;

                case BlogEntityType.Comments:
                    entityCount = _blogCommentsRepository.GetAll().Count();
                    break;
            }

            return entityCount;
        }
    }
}
