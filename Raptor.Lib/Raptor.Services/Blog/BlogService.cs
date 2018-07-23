using Raptor.Data.Core;
using Raptor.Data.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

            return _blogPostsRepository
                .IncludeMultiple(b => b.PostCategory, b => b.Comments, b => b.CreatedBy)
                .SingleOrDefault(b => b.BlogPostId == blogPostId);
        }

        public BlogPost GetBlogPostByLink(string link){
            return _blogPostsRepository
                .IncludeMultiple(b => b.PostCategory, b => b.Comments, b => b.CreatedBy, b => b.CreatedBy.SocialProfile,b => b.Comments)
                .SingleOrDefault(b => b.Link == link);
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

        public IEnumerable<BlogPost> SearchBlogPosts(string query) {
            return _blogPostsRepository.Find(b => b.Title.ToLower().Contains(query)).ToList();
        }

        public IList<BlogComment> GetBlogCommentsById(int blogPostId) {
            return _blogCommentsRepository.Find(c => c.BlogPostId == blogPostId).ToList();
        }

        public IList<BlogComment> GetBlogCommentsByIds(int[] blogPostIds) {
            return _blogCommentsRepository.Find(c => blogPostIds.Contains(c.BlogPostId)).ToList();
        }

        public int GetBlogCommentsCount(int blogPostId, bool isApproved = true) {
            return _blogCommentsRepository.Find(c => c.BlogPostId == blogPostId && c.Approved == isApproved).Count();
        }

        public void DeleteBlogComment(BlogComment comment) {
            _blogCommentsRepository.Delete(comment);
        }

        public void DeleteBlogComments(IList<BlogComment> comments) {
            _blogCommentsRepository.DeleteRange(comments);
        }

        public void CreateBlogComent(BlogComment comment){
            _blogCommentsRepository.Create(comment);
        }

        public IList<BlogPostCategory> GetBlogPostCategories(bool includePosts = false) {
            return includePosts ? _blogPostCategoriesRepository.IncludeMultiple(b => b.Posts).ToList() : _blogPostCategoriesRepository.GetAll().ToList();
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

        public void CreateBlogPostCategory(BlogPostCategory category) {
            var checkCategory = _blogPostCategoriesRepository.SingleOrDefault(c => c.Slug == category.Slug);
            if (checkCategory != null) throw new ArgumentException("A blog post category already exists for the specified slug");

            _blogPostCategoriesRepository.Create(category);
        }

        public void UpdateBlogPostCategory(BlogPostCategory category) {
            _blogPostCategoriesRepository.Update(category);
        }

        public bool BlogPostCategoryExists(int categoryId) {
            return _blogPostCategoriesRepository.Any(categoryId);
        }

        public void CreateBlogPostCategory(string name, string slug, string description) {
            var category = new BlogPostCategory() {
                Name = name,
                Slug = slug,
                Description = description
            };

            CreateBlogPostCategory(category);
        }

        public BlogPostCategory GetBlogPostCategoryById(int id) {
            return _blogPostCategoriesRepository.GetById(id);
        }

        public BlogPostCategory GetBlogPostCategoryBySlug(string slug) {
            return _blogPostCategoriesRepository.SingleOrDefault(c => c.Slug == slug);
        }

        public IEnumerable<BlogComment> GetAllBlogComments(){
            return _blogCommentsRepository.IncludeMultiple(c => c.BlogPost, c => c.Person).ToList();
        }

        public IEnumerable<BlogPost> GetBlogPosts(int categoryId = 0, int mostRecentCount = 0) {
            var blogPosts = categoryId == 0 ?
                _blogPostsRepository.IncludeMultiple(b => b.PostCategory, b => b.CreatedBy).AsEnumerable().Select(b => b).ToList() :
                _blogPostsRepository.IncludeMultiple(b => b.PostCategory, b => b.CreatedBy).Where(b => b.PostCategoryId == categoryId).ToList();

            if (mostRecentCount != 0)
                blogPosts = blogPosts.OrderBy(b => b.DateCreatedUtc).Take(mostRecentCount).ToList();

            return blogPosts;
        }
    }
}
