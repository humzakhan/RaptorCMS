using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raptor.Services.Blog;
using Raptor.Web.Models;

namespace Raptor.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly string _blogPostsListsView = "BlogPostsList";
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService) {
            _blogService = blogService;
        }

        public IActionResult Index() {
            var model = new BlogListViewModel() {
                RecentPosts = _blogService.GetBlogPosts(mostRecentCount: 3).ToList(),
                AllPosts = _blogService.GetBlogPosts().ToList(),
                Categories = _blogService.GetBlogPostCategories().ToList(),
                Category = "All"
            };

            return View(_blogPostsListsView, model);
        }

        [Route("blog/category/{slug}")]
        public IActionResult BlogPostsByCategory(string slug) {
            var category = _blogService.GetBlogPostCategoryBySlug(slug);

            if (category == null) {
                return RedirectToAction("Index");
            }

            var model = new BlogListViewModel()
            {
                RecentPosts = _blogService.GetBlogPosts(mostRecentCount: 3).ToList(),
                AllPosts = _blogService.GetBlogPosts(categoryId: category.PostCategoryId).ToList(),
                Categories = _blogService.GetBlogPostCategories(),
                Category = category.Name
            };

            return View(_blogPostsListsView, model);
        }
    }
}