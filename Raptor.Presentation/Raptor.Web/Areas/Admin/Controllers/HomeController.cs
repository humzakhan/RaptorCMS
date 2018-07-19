using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raptor.Core.Helpers;
using Raptor.Data.Models;
using Raptor.Data.Models.Blog;
using Raptor.Data.Models.Configuration;
using Raptor.Data.Models.Logging;
using Raptor.Services.Blog;
using Raptor.Services.Configuration;
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
        private readonly ISettingService _settingsService;

        public HomeController(IBlogService blogService, IUserService userService, ILogService logFactory, IWorkContext workContext, ICustomerActivityService activityService, ISettingService settingsService) {
            _blogService = blogService;
            _userService = userService;
            _logsFactory = logFactory;
            _workContext = workContext;
            _activityService = activityService;
            _settingsService = settingsService;
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
            var model = new SystemInfoViewModel() {
                Version = _settingsService.GetSettingByKey(SettingsConstants.RaptorVersion).Value,
                AspNetVersion = Environment.Version.ToString(),
                ServerTimeZone = TimeZoneInfo.Utc.ToString(),
                LocalTimeZone = TimeZoneInfo.Local.ToString(),
                OperatingSystem = Environment.OSVersion.ToString(),
                Host = HttpContext.Request.Host.ToString()
            };

            return View(model);
        }

        [HttpGet]
        [Route("logs")]
        public IActionResult Logs() {
            var model = new LogViewModel() {
                Logs = _logsFactory.GetAllLogs().ToList()
            };

            return View(model);
        }

        [Route("logs")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logs(LogViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                model.Logs = _logsFactory.SearchLogs(DateTime.Parse(model.DateFrom), DateTime.Parse(model.DateTo), model.LogLevel).ToList();
                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.ViewLogs, $"Viewed logs from {model.DateFrom} to {model.DateTo} for level {model.LogLevel}");
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"An error occurred when searching for logs. {ex.Message}");
                _logsFactory.InsertLog(LogLevel.Error, $"An error occurred when searching for logs. {ex.Message}", ex.ToString());
            }

            return View(model);
        }

        [Route("admin/logs/{logId}")]
        public IActionResult ViewLog(int logId) {
            try {
                var logEntry = _logsFactory.GetLogById(logId);

                if (logEntry == null) return RedirectToAction("Logs");

                return View("LogView", logEntry);
            }
            catch (Exception ex) {
                _logsFactory.InsertLog(LogLevel.Error, "An error occurred when tried to fetch log.", ex.Message);
                return RedirectToAction("Logs");
            }
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