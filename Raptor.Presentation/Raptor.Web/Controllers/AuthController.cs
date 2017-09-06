using Microsoft.AspNetCore.Mvc;
using Raptor.Services.Users;

namespace Raptor.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService) {
            _userService = userService;
        }

        public IActionResult Index() {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }
    }
}