using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raptor.Services.Helpers;
using Raptor.Services.Logging;
using Raptor.Services.Users;
using Raptor.Web.Areas.Admin.ViewModels;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerActivityService _activityService;
        private readonly ILogService _logFactory;

        public UsersController(IUserService userService, IWorkContext workContext, ICustomerActivityService activityService, ILogService logFactory) {
            _userService = userService;
            _workContext = workContext;
            _activityService = activityService;
            _logFactory = logFactory;
        }

        public IActionResult Index() {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create() {
            var model = new UserViewModel() {
                Title = "Add Users",
                Action = "create"
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Roles() {
            return View("Roles");
        }

        [Route("admin/users/roles/create")]
        [HttpGet]
        public IActionResult CreateRole() {
            var model = new RoleViewModel() {
                Title = "Create Role",
                Action = "CreateRole"
            };

            return View("CreateRole", model);
        }
    }
}