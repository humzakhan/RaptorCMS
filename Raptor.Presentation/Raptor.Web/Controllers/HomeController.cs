using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Raptor.Data.Models.Blog;
using Raptor.Data.Models.Logging;
using Raptor.Services.Blog;
using Raptor.Services.Helpers;
using Raptor.Services.Logging;
using Raptor.Web.Models;
using Raptor.Web.ViewModels;

namespace Raptor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILogService _logService;
        private readonly IWorkContext _workContext;
        private readonly string _blogPostsListsView = "BlogPostsList";

        public HomeController(IBlogService blogService, ILogService logService, IWorkContext workContext){
            _blogService = blogService;
            _logService = logService;
            _workContext = workContext;
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
                if (post != null) {
                    var model = new DisplayPostViewModel() {
                        BlogPost = post,
                        PostCategoryName = _blogService.GetBlogPostCategoryById(post.PostCategoryId).Name,
                        RecentPosts = _blogService.GetBlogPosts(mostRecentCount: 3).ToList(),
                        Categories = _blogService.GetBlogPostCategories(includePosts: true).OrderByDescending(b => b.Posts.Count).Take(5).ToList()
                    };

                    return View(model);
                }
            }
            catch (Exception ex) {
                _logService.InsertLog(LogLevel.Error, "Unable to load blog post. " + ex.Message, ex.ToString());

            }

            return RedirectToAction("index", "home");
        }
        
        [HttpPost]
        [Route("/publish/comment")]
        public IActionResult PublishComment(CommentViewModel model){
            try {
                var blogComment = new BlogComment() {
                    BlogPostId = model.BlogPostId,
                    Content = model.CommentContent,
                    BusinessEntityId = _workContext.CurrentUser.BusinessEntityId,
                    AuthorIp = HttpContext.Connection.RemoteIpAddress.ToString(),
                    Agent = HttpContext.Request.Headers["User-Agent"],
                    Approved = true,
                    DateCreatedGmt = DateTime.UtcNow,
                    DateCreated = DateTime.Now
                };

                _blogService.CreateBlogComent(blogComment);

                var blogPost = _blogService.GetBlogPostById(model.BlogPostId);

                blogPost.CommentsCount++;
                _blogService.UpdateBlogPost(blogPost);

                return new JsonResult(new { Status = true, Message = "Your comment has been published successfully." });
            }
            catch (Exception ex) {
                _logService.InsertLog(LogLevel.Error, "Unable to create new comment", ex.ToString());
                return new JsonResult(new { Status = false, Message = ex.Message});
            }
        }

        [Route("contact")]
        public IActionResult Contact() {
            return View();
        }

        [Route("search")]
        [HttpPost]
        public IActionResult Search(string query) {
            var results = _blogService.SearchBlogPosts(query);
            var model = new BlogListViewModel()
            {
                RecentPosts = _blogService.GetBlogPosts(mostRecentCount: 3).ToList(),
                AllPosts = results.ToList(),
                Categories = _blogService.GetBlogPostCategories().ToList(),
                Category = "Search Results for: " + query
            };

            return View("~/Views/Blog/BlogPostsList.cshtml", model);
        }
    }
}