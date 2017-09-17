using Microsoft.AspNetCore.Mvc;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index() {
            return View();
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