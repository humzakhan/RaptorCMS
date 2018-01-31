using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public BlogController(IBlogService blogService, ILogService logService, ICustomerActivityService activityService, IWorkContext workContext) {
            _blogService = blogService;
            _logService = logService;
            _activityService = activityService;
            _workContext = workContext;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        [Route("admin/blog/posts/create")]
        public IActionResult Create() {
            var model = new BlogPostViewModel() {
                PageTitle = "Create Blog Post",
                Action = "create",
                BlogPostCategories = new SelectList(_blogService.GetBlogPostCategories().ToList(), "PostCategoryId", "Name")
            };

            return View("BlogPostView", model);
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
    }
}