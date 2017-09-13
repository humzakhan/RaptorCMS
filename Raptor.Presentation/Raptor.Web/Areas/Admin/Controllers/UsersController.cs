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

        public IActionResult Create() {
            var model = new UserViewModel() {
                Title = "Add Users",
                Action = "create"
            };

            return View(model);
        }
    }
}