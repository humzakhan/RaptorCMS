using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raptor.Data.Models.Logging;
using Raptor.Data.Models.Users;
using Raptor.Services.Helpers;
using Raptor.Services.Logging;
using Raptor.Services.Users;
using Raptor.Web.Areas.Admin.ViewModels;
using System;

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
        private readonly IUserRegisterationService _userRegisterationService;
        private readonly string _userAccountView;

        public UsersController(IUserService userService, IWorkContext workContext, ICustomerActivityService activityService, ILogService logFactory, IUserRegisterationService userRegisterationService) {
            _userService = userService;
            _workContext = workContext;
            _activityService = activityService;
            _logFactory = logFactory;
            _userRegisterationService = userRegisterationService;
            _userAccountView = "CreateUserView";
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

            return View(_userAccountView, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel model) {
            if (!ModelState.IsValid) return View(_userAccountView, model);

            try {
                var user = AutoMapper.Mapper.Map<UserViewModel, Person>(model);
                var result = _userRegisterationService.Register(user, model.Password);
                if (!result.Success) {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error);
                        return View(_userAccountView, model);
                    }
                }

                model.Title = "Add Users";
                ViewBag.Status = "OK";
                ViewBag.Message = "New user account has been created successfully";
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to create new user: {ex.Message}");
                _logFactory.InsertLog(LogLevel.Error, $"Unable to create new user: {ex.Message}", ex.ToString());
            }

            return View(_userAccountView, model);

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