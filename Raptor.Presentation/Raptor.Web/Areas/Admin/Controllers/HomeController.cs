using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raptor.Services.Blog;
using Raptor.Services.Logging;
using Raptor.Services.Users;
using Raptor.Web.Areas.Admin.ViewModels;
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
        private readonly ILogService _logsService;

        public HomeController(IBlogService blogService, IUserService userService, ILogService logsService) {
            _blogService = blogService;
            _userService = userService;
            _logsService = logsService;
        }
        public IActionResult Index() {
            var model = new DashboardViewModel() {
                PostsCount = _blogService.CountEntities(BlogEntityType.Posts),
                CommentsCount = _blogService.CountEntities(BlogEntityType.Comments),
                UsersCount = _userService.CountUsers(),
                RecentLogs = _logsService.GetAllLogs(recentNo: 5).ToList()
            };

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
    }
}