using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raptor.Web.Areas.Admin.ViewModels;

namespace Raptor.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("admin/blog/posts/create")]
        public IActionResult Create() {
            var model = new BlogPostViewModel() {
                PageTitle = "Create Blog Post",
                Action = "create"
            };

            return View("BlogPostView", model);
        }
    }
}