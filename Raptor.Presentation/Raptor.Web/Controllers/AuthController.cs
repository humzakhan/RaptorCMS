using Microsoft.AspNetCore.Mvc;
using Raptor.Core.Helpers;
using Raptor.Data.Models.Logging;
using Raptor.Data.Models.Users;
using Raptor.Services.Authentication;
using Raptor.Services.Helpers;
using Raptor.Services.Logging;
using Raptor.Services.Users;
using Raptor.Web.ViewModels;
using System;

namespace Raptor.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRegisterationService _userRegisterationService;
        private readonly IUserAuthenticationService _authService;
        private readonly ICustomerActivityService _activityService;
        private readonly ILogService _logService;
        private readonly IWorkContext _workContext;

        public AuthController(IUserService userService, IUserRegisterationService userRegisterationService, IUserAuthenticationService authService, ICustomerActivityService activityService, ILogService logService, IWorkContext workContext) {
            _userService = userService;
            _userRegisterationService = userRegisterationService;
            _authService = authService;
            _activityService = activityService;
            _logService = logService;
            _workContext = workContext;
        }

        public IActionResult Index() {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "") {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl = "") {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid) return View(model);

            var isEmailAddress = CommonHelper.IsValidEmail(model.UsernameOrEmailAddress);

            var userExists = isEmailAddress
                ? _userService.CheckIfUserExistsByEmail(model.UsernameOrEmailAddress)
                : _userService.CheckIfUserExistsByUsername(model.UsernameOrEmailAddress);

            if (!userExists) {
                ModelState.AddModelError("", "No user found for the specified username / email address.");
                return View(model);
            }

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            var loginResult = _userRegisterationService.ValidateUser(model.UsernameOrEmailAddress, model.Password);
            switch (loginResult) {
                case UserLoginResults.Deleted:
                case UserLoginResults.UserNotExists:
                case UserLoginResults.NotActive:
                    ModelState.AddModelError("", "No user found for the specified username / email address.");
                    break;

                case UserLoginResults.WrongPassword:
                    ModelState.AddModelError("", "Incorrect password, please try again.");
                    break;

                case UserLoginResults.Successful:
                    _authService.SignIn(model.UsernameOrEmailAddress);
                    if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout() {
            _authService.SignOut();
            _workContext.CurrentUser = null;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                var user = _userService.GetUserByEmail(model.EmailAddress);
                if (user != null) {
                    _userService.CreateForgotPasswordRequest(user.BusinessEntityId);
                    ViewBag.Status = "OK";
                    ViewBag.Message = "Please check your email for instructions on how to reset your password";
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
                _logService.InsertLog(LogLevel.Error, ex.Message, ex.ToString());
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string link) {
            var forgotPasswordRequest = _userService.ValidateForgotPasswordRequest(link);
            if (string.IsNullOrEmpty(link) || !forgotPasswordRequest) return RedirectToAction("Login");

            ViewBag.Link = link;
            return View("ResetPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                var forgotPasswordRequest = _userService.GetForgotPasswordRequest(model.Link);
                var user = _userService.GetUserById(forgotPasswordRequest.BusinessEntityId);

                _userService.UpdatePassword(user.EmailAddress, model.Password);

                ViewBag.Status = "OK";
                ViewBag.Message = "Your password has been updated";
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
                _logService.InsertLog(LogLevel.Error, ex.Message, ex.ToString());
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                var userRegistrationResult = _userRegisterationService.Register(model.FirstName, model.LastName, model.EmailAddress, model.Username, model.Password);

                if (userRegistrationResult.Success) {
                    ViewBag.Status = "OK";
                    ViewBag.Message = "Your account has been successfully created!";
                }
                else {
                    foreach (var error in userRegistrationResult.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
                _logService.InsertLog(LogLevel.Error, ex.Message, ex.ToString());
            }

            return View(model);
        }
    }
}