using Microsoft.AspNetCore.Mvc;
using Raptor.Web.Areas.Admin.ViewModels;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public IActionResult Index() {
            return View();
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