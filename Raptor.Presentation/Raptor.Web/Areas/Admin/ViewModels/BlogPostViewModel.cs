using Microsoft.AspNetCore.Mvc.Rendering;
using Raptor.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class BlogPostViewModel
    {
        public string PageTitle { get; set; }
        public string Action { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Category")]
        public int BlogPostCategoryId { get; set; }

        [Display(Name = "Status")]
        public PostStatus PostStatus { get; set; }

        [Display(Name = "Allow Comments?")]
        public bool IsCommentsAllowed { get; set; }

        public string Password { get; set; }

        public SelectList BlogPostCategories { get; set; }
    }
}
