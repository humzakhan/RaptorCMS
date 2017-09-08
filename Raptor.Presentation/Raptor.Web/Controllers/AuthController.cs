using Microsoft.AspNetCore.Mvc;
using Raptor.Core.Helpers;
using Raptor.Data.Models.Users;
using Raptor.Services.Users;
using Raptor.Web.ViewModels;

namespace Raptor.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRegisterationService _userRegisterationService;

        public AuthController(IUserService userService, IUserRegisterationService userRegisterationService) {
            _userService = userService;
            _userRegisterationService = userRegisterationService;
        }

        public IActionResult Index() {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "") {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl = "") {
            if (!ModelState.IsValid) return View(model);

            var isEmailAddress = CommonHelper.IsValidEmail(model.UsernameOrEmailAddress);

            var userExists = isEmailAddress
                ? _userService.CheckIfUserExistsByEmail(model.UsernameOrEmailAddress)
                : _userService.CheckIfUserExistsByUsername(model.UsernameOrEmailAddress);

            if (!userExists) {
                ModelState.AddModelError("", "No user found for the specified username / email address.");
                return View(model);
            }

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
                    if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword() {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword() {
            return View("ResetPassword");
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }
    }
}