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
        private readonly IUserRolesService _userRolesService;
        private readonly string _userAccountView;
        private readonly string _userRoleView;

        public UsersController(IUserService userService, IWorkContext workContext, ICustomerActivityService activityService, ILogService logFactory, IUserRegisterationService userRegisterationService, IUserRolesService userRolesService) {
            _userService = userService;
            _workContext = workContext;
            _activityService = activityService;
            _logFactory = logFactory;
            _userRegisterationService = userRegisterationService;
            _userRolesService = userRolesService;
            _userAccountView = "CreateUserView";
            _userRoleView = "RoleView";
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
        public IActionResult Edit(int id) {
            if (id == 0) return RedirectToAction("Index");
            var model = new UserViewModel();

            try {
                var user = _userService.GetUserById(id);
                if (user != null) {
                    model = AutoMapper.Mapper.Map<Person, UserViewModel>(user);

                    model.Title = "Edit User";
                    model.Action = "Edit";
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to load the specified user: {ex.Message}");
                _logFactory.InsertLog(LogLevel.Error, $"Unable to load the specified user: {ex.Message}", ex.ToString());
            }

            return View(_userAccountView, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel model) {
            if (!ModelState.IsValid) return View(_userAccountView, model);

            try {
                var user = _userService.GetUserById(model.BusinessEntityId);

                if (user.Username != model.Username) {
                    // User attempted to change username, run the necessary verifications
                    var usernameRegistered = _userService.CheckIfUserExistsByUsername(model.Username);

                    if (usernameRegistered) {
                        ModelState.AddModelError("", "The username is not available.");
                        return View(_userAccountView, model);
                    }
                }

                if (user.EmailAddress != model.EmailAddress) {
                    // User attempted to change their email address, run the necessary verifications
                    var emailAddressRegistered = _userService.CheckIfUserExistsByEmail(model.EmailAddress);

                    if (emailAddressRegistered) {
                        ModelState.AddModelError("", "The email address is already registered.");
                        return View(_userAccountView, model);
                    }
                }

                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.DisplayName = $"{model.FirstName} {model.LastName}";
                user.Username = model.Username;
                user.EmailAddress = model.EmailAddress;
                user.About = model.About;
                user.Website = model.Website;
                user.IsBlocked = model.IsBlocked;
                user.IsEmailVerified = model.IsEmailVerified;
                user.IsDeleted = model.IsDeleted;

                _userService.UpdateUser(user);

                model.Title = "Edit User";
                ViewBag.Status = "OK";
                ViewBag.Message = "Your changes have been saved succesfully.";
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to edit user: {ex.Message}");
                _logFactory.InsertLog(LogLevel.Error, $"Unable to edit user: {ex.Message}", ex.ToString());
            }

            return View(_userAccountView, model);
        }

        [HttpGet]
        public IActionResult Roles() {
            var roles = _userRolesService.GetAllUserRoles();
            return View("Roles", roles);
        }

        [HttpGet]
        [Route("admin/users/roles/edit")]
        public IActionResult EditRole(int id) {
            var role = _userRolesService.GetUserRoleById(id);
            if (role == null) return RedirectToAction("Roles");

            var model = AutoMapper.Mapper.Map<Role, RoleViewModel>(role);

            model.Title = "Edit Role";
            model.Action = "EditRole";

            return View(_userRoleView, model);
        }

        [HttpPost]
        [Route("admin/users/roles/edit")]
        public IActionResult EditRole(RoleViewModel model) {
            if (!ModelState.IsValid) return View(_userRoleView, model);

            try {
                var role = _userRolesService.GetUserRoleById(model.RoleId);

                if (role.SystemKeyword != model.SystemKeyword) {
                    // User attempted to change system keyword for the role. Run necessary verifications
                    var existingRole = _userRolesService.GetUserRoleByKeyword(model.SystemKeyword);
                    if (existingRole != null) {
                        model.Title = "Edit Role";
                        ModelState.AddModelError("", "System Keyword is not available, please try something else.");
                        return View(_userRoleView, model);
                    }

                    role.DisplayName = model.DisplayName;
                    role.SystemKeyword = model.SystemKeyword;

                    model.Title = "Edit Role";
                    ViewBag.Status = "OK";
                    ViewBag.Message = "Your changes have been saved successfully.";

                    _userRolesService.UpdateUserRole(role);
                    _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.EditRoles, $"Edited user role: {role.SystemKeyword}");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to save changes to role: {model.SystemKeyword}");
                _logFactory.InsertLog(LogLevel.Error, $"Unable to save changes to role: {model.SystemKeyword} because {ex.Message}", ex.ToString());
            }

            return View(_userRoleView, model);
        }

        [Route("admin/users/roles/create")]
        [HttpGet]
        public IActionResult CreateRole() {
            var model = new RoleViewModel() {
                Title = "Create Role",
                Action = "CreateRole"
            };

            return View(_userRoleView, model);
        }

        [Route("admin/users/roles/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRole(RoleViewModel model) {
            if (!ModelState.IsValid) return View(_userRoleView, model);

            try {
                var existingRole = _userRolesService.GetUserRoleByKeyword(model.SystemKeyword);
                if (existingRole != null) {
                    ModelState.AddModelError("", $"Unable to create user role with system keyword: {model.SystemKeyword} because the system keyword is not available. Try something else");
                    return View(_userRoleView, model);
                }

                var role = AutoMapper.Mapper.Map<RoleViewModel, Role>(model);

                _userRolesService.CreateUserRole(role);
                _activityService.InsertActivity(_workContext.CurrentUser.BusinessEntity, ActivityLogDefaults.CreateRole, $"Created user role: {role.SystemKeyword}");


                model.Title = "Create Role";
                ViewBag.Status = "OK";
                ViewBag.Message = "New role has been created successfully.";
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Unable to create user role: {ex.Message}");
                _logFactory.InsertLog(LogLevel.Error, $"Unable to create user role: {ex.Message}", ex.ToString());
            }

            return View(_userRoleView, model);
        }
    }
}