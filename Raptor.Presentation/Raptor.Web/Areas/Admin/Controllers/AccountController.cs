using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raptor.Core.Helpers;
using Raptor.Data.Models.Users;
using Raptor.Services.Users;
using Raptor.Web.Areas.Admin.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Settings() {
            var emailClaim = User.Claims.FirstOrDefault(c => c.ValueType == ClaimTypes.Email);

            if (emailClaim == null) return null;

            var currentUser = _userService.GetUserByEmail(emailClaim.Value);
            var userViewModel = AutoMapper.Mapper.Map<Person, UserViewModel>(currentUser);

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(UserViewModel model) {
            if (!ModelState.IsValid) return View(model);

            try {
                var emailClaim = User.Claims.FirstOrDefault(c => c.ValueType == ClaimTypes.Email);
                if (emailClaim == null) return null;

                var currentUser = _userService.GetUserByEmail(emailClaim.Value);

                // Check if it is a valid email address
                if (!CommonHelper.IsValidEmail(model.EmailAddress)) {
                    ModelState.AddModelError("", "Email Address is not valid.");
                    return View(model);
                }

                // Check if the user updated their email address
                if (model.EmailAddress != currentUser.EmailAddress) {
                    var emailExists = _userService.CheckIfUserExistsByEmail(model.EmailAddress);
                    if (emailExists) {
                        ModelState.AddModelError("", "A user with a similar email address already exists.");
                        return View(model);
                    }
                }

                // Check if the user updated their username
                if (model.Username != currentUser.Username) {
                    var userExists = _userService.CheckIfUserExistsByUsername(model.Username);
                    if (userExists) {
                        ModelState.AddModelError("", "Username not available.");
                        return View(model);
                    }
                }

                currentUser.FirstName = model.FirstName;
                currentUser.MiddleName = model.MiddleName;
                currentUser.LastName = model.LastName;
                currentUser.DisplayName = model.DisplayName;
                currentUser.EmailAddress = model.EmailAddress;
                currentUser.Username = model.Username;
                currentUser.About = model.About;
                currentUser.Website = model.Website;



                _userService.UpdateUser(currentUser);

                ViewBag.Status = "OK";
                ViewBag.Message = "Your changes have been saved successfully.";
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.ToString());
            }

            return View(model);
        }
    }
}