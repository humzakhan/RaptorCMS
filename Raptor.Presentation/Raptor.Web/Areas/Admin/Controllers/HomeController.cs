using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raptor.Core.Helpers;
using Raptor.Data.Models;
using Raptor.Data.Models.Blog;
using Raptor.Data.Models.Logging;
using Raptor.Services.Blog;
using Raptor.Services.Helpers;
using Raptor.Services.Logging;
using Raptor.Services.Users;
using Raptor.Web.Areas.Admin.ViewModels;
using System;
using System.Linq;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly ILogService _logsFactory;
        private readonly IWorkContext _workContext;
        private readonly ICustomerActivityService _activityService;

        public HomeController(IBlogService blogService, IUserService userService, ILogService logFactory, IWorkContext workContext, ICustomerActivityService activityService) {
            _blogService = blogService;
            _userService = userService;
            _logsFactory = logFactory;
            _workContext = workContext;
            _activityService = activityService;
        }

        public IActionResult Index() {
            var model = new DashboardViewModel();

            try {
                model = new DashboardViewModel() {
                    PostsCount = _blogService.CountEntities(BlogEntityType.Posts),
                    CommentsCount = _blogService.CountEntities(BlogEntityType.Comments),
                    UsersCount = _userService.CountUsers(),
                    RecentLogs = _logsFactory.GetAllLogs(recentNo: 5).ToList()
                };

            }
            catch (Exception ex) {
                _logsFactory.InsertLog(LogLevel.Error, ex.Message, ex.ToString());
            }

            return View(model);
        }

        [HttpGet]
        [Route("system")]
        public IActionResult SystemInformation() {
            return View();
        }

        [HttpGet]
        [Route("logs")]
        public IActionResult Logs() {
            return View();
        }

        [HttpPost]
        public IActionResult QuickDraft(DashboardViewModel model) {
            try {
                if (string.IsNullOrEmpty(model.DraftTitle) || string.IsNullOrWhiteSpace(model.DraftContent))
                    return Json(new { response = "error", message = "Title/Content is null or empty." });

                var blogPost = new BlogPost() {
                    DateCreatedUtc = DateTime.UtcNow,
                    Content = model.DraftContent,
                    Title = model.DraftTitle,
                    Excerpt = model.DraftContent.Length > 200 ? model.DraftContent.Substring(0, 200) : model.DraftContent,
                    Status = PostStatus.Draft,
                    IsCommentsAllowed = true,
                    Password = string.Empty,
                    Name = string.Empty,
                    DateModifiedUtc = DateTime.UtcNow,
                    Guid = Guid.NewGuid(),
                    Link = CommonHelper.GenerateLinkForBlogPost(model.DraftTitle),
                    PostType = PostType.Post,
                    PostCategoryId = 1,
                    CreatedById = _workContext.CurrentUser.BusinessEntityId
                };

                _blogService.CreateBlogPost(blogPost);
                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.AddBlogPost, $"Created blog post: {model.DraftTitle}");

            }
            catch (Exception ex) {
                _logsFactory.InsertLog(LogLevel.Error, $"Unable to create quick draft: {ex.Message}", ex.ToString());
                return Json(new { response = "error", message = $"Unable to create quick draft: {ex.Message}" });
            }

            return Json(new { response = "success", message = "Draft created successfully." });
        }
    }
}