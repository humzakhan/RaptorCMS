using Microsoft.AspNetCore.Mvc;

namespace Raptor.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}