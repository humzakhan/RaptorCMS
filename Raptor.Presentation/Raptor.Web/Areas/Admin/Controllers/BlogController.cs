using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Raptor.Core.Helpers;
using Raptor.Data.Models;
using Raptor.Data.Models.Blog;
using Raptor.Data.Models.Logging;
using Raptor.Services.Blog;
using Raptor.Services.Helpers;
using Raptor.Services.Logging;
using Raptor.Web.Areas.Admin.ViewModels;
using System;
using System.Linq;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILogService _logService;
        private readonly ICustomerActivityService _activityService;
        private readonly IWorkContext _workContext;
        private readonly string _blogPostView;

        public BlogController(IBlogService blogService, ILogService logService, ICustomerActivityService activityService, IWorkContext workContext) {
            _blogService = blogService;
            _logService = logService;
            _activityService = activityService;
            _workContext = workContext;
            _blogPostView = "BlogPostView";
        }

        public IActionResult Index() {
            var blogPosts = _blogService.GetBlogPosts();
            return View(blogPosts);
        }

        [HttpGet]
        [Route("admin/blog/posts/create")]
        public IActionResult Create() {
            var model = new BlogPostViewModel() {
                PageTitle = "Create Blog Post",
                Action = "create",
                BlogPostCategories = new SelectList(_blogService.GetBlogPostCategories().ToList(), "PostCategoryId", "Name")
            };

            return View(_blogPostView, model);
        }

        [HttpPost]
        [Route("admin/blog/posts/create")]
        public IActionResult Create(BlogPostViewModel model) {
            if (!ModelState.IsValid) return View(_blogPostView, model);

            try {
                var blogPost = new BlogPost() {
                    DateCreatedUtc = DateTime.UtcNow,
                    Content = model.Content,
                    Title = model.Title,
                    Excerpt = model.Content.Substring(0, 150),
                    Status = model.PostStatus,
                    IsCommentsAllowed = model.IsCommentsAllowed,
                    Password = model.Password,
                    DateModifiedUtc = DateTime.UtcNow,
                    Guid = Guid.NewGuid(),
                    Link = CommonHelper.GenerateLinkForBlogPost(model.Title),
                    PostType = PostType.Post,
                    CommentsCount = 0,
                    PostCategoryId = model.BlogPostCategoryId,
                    CreatedById = _workContext.CurrentUser.BusinessEntityId
                };

                _blogService.CreateBlogPost(blogPost);
                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.UpdateBlogPost, "Created blog post: {0}", model.Title);

                ViewBag.Status = "OK";
                ViewBag.Message = $"Blog post published: {model.Title}";
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to add new blog post: {ex.Message}");
                _logService.InsertLog(LogLevel.Error, $"Unable to add new blog post: {ex.Message}", ex.ToString());
            }

            model.PageTitle = "Create Blog Post";
            return View(_blogPostView, model);
        }

        [HttpGet]
        public IActionResult Categories() {
            var model = new BlogPostCategoryViewModel() {
                BlogPostCategories = _blogService.GetBlogPostCategories(),
                PostAction = "categories"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Categories(BlogPostCategoryViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                _blogService.CreateBlogPostCategory(model.Name, model.Slug, model.Description);
                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.AddBlogPostCategory, "Created new blog post category: {0}", model.Name);

                ViewBag.Status = "OK";
                ViewBag.Message = "New blog post category successfully added";

                model.BlogPostCategories = _blogService.GetBlogPostCategories();
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to create new blog post category: {ex.Message}");
                _logService.InsertLog(LogLevel.Error, ex.Message, ex.ToString());

            }

            return View(model);
        }

        [HttpGet]
        [Route("admin/blog/categories/update")]
        public IActionResult UpdateBlogPostCategory(int id) {
            var blogPostCategory = _blogService.GetBlogPostCategoryById(id);
            if (blogPostCategory == null) return RedirectToAction("categories");

            var model = new BlogPostCategoryViewModel() {
                PostAction = "UpdateBlogPostCategory",
                Name = blogPostCategory.Name,
                Slug = blogPostCategory.Slug,
                Description = blogPostCategory.Description,
                CategoryId = id
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/blog/categories/update")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateBlogPostCategory(BlogPostCategoryViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                var category = _blogService.GetBlogPostCategoryById(model.CategoryId);
                if (category == null) {
                    ModelState.AddModelError("", "Invalid blog post category id specified");
                    return View(model);
                }

                category.Name = model.Name;
                category.Slug = model.Slug;
                category.Description = model.Description;

                _blogService.UpdateBlogPostCategory(category);
                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.EditBlogPostCategory, "Updated blog post category: {0} - {1}", model.CategoryId, model.Name);

                ViewBag.Status = "OK";
                ViewBag.Message = "Your changes have been saved successfully.";
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to update blog post category: {ex.Message}");
                _logService.InsertLog(LogLevel.Error, ex.Message, ex.ToString());
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Comments() {
            var comments = _blogService.GetAllBlogComments();
            return View(comments);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var blogPost = _blogService.GetBlogPostById(id);

            if (blogPost == null) return RedirectToAction("Index");

            var model = new BlogPostViewModel() {
                BlogPostId = blogPost.BlogPostId,
                Title = blogPost.Title,
                Content = blogPost.Content,
                BlogPostCategoryId = blogPost.PostCategoryId,
                Password = blogPost.Password,
                IsCommentsAllowed = blogPost.IsCommentsAllowed,
                Action = "Edit",
                PageTitle = "Edit blog post",
                BlogPostCategories = new SelectList(_blogService.GetBlogPostCategories().ToList(), "PostCategoryId", "Name"),
                PostStatus = blogPost.Status
            };

            return View(_blogPostView, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogPostViewModel model) {
            if (!ModelState.IsValid) return View(_blogPostView, model);

            var blogPost = _blogService.GetBlogPostById(model.BlogPostId);

            if (blogPost == null) {
                ModelState.AddModelError("", "Invalid blog post id, cannot update.");
                return View(_blogPostView, model);
            }

            try {
                blogPost.Title = model.Title;
                blogPost.Content = model.Content;
                blogPost.PostCategoryId = model.BlogPostCategoryId;
                blogPost.IsCommentsAllowed = model.IsCommentsAllowed;
                blogPost.Password = model.Password;
                blogPost.Status = model.PostStatus;

                _blogService.UpdateBlogPost(blogPost);
                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.UpdateBlogPost, "Updated blog post, id: {0}, title: {1}", model.BlogPostId, model.Title);

                ViewBag.Status = "OK";
                ViewBag.Message = "Your changes have been saved successfully.";
            }
            catch (Exception ex) {
                _logService.InsertLog(LogLevel.Error, $"Unable to attend blog post: {ex.Message}", ex.ToString());
                ModelState.AddModelError("", $"Unable to update blog: {ex.Message}");
            }

            return View(_blogPostView, model);
        }
    }
}