using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Raptor.Services.Blog;
using Raptor.Web.Models;

namespace Raptor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IActionResult Index()
        {
            var posts = _blogService.GetBlogPosts(mostRecentCount: 3);
            var model = new HomeViewModel() {
                MostRecentPosts = posts.ToList()
            };

            return View(model);
        }
    }
}