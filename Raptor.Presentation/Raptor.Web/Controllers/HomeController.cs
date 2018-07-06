using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Raptor.Data.Models.Logging;
using Raptor.Services.Blog;
using Raptor.Services.Logging;
using Raptor.Web.Models;

namespace Raptor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILogService _logService;

        public HomeController(IBlogService blogService, ILogService logService){
            _blogService = blogService;
            _logService = logService;
        }

        public IActionResult Index()
        {
            var posts = _blogService.GetBlogPosts(mostRecentCount: 3);
            var model = new HomeViewModel() {
                MostRecentPosts = posts.ToList()
            };

            return View(model);
        }

        [Route("/blog/{link}")]
        public IActionResult BlogPost(string link){
            try {
                var post = _blogService.GetBlogPostByLink(link);
                if (post != null) return View(post);
            }
            catch (Exception ex) {
                _logService.InsertLog(LogLevel.Error, "Unable to load blog post", ex.ToString());

            }

            return RedirectToAction("index", "home");
        }
    }
}