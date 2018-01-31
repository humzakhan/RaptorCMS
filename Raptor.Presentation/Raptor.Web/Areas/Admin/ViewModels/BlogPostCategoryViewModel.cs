using Raptor.Data.Models.Blog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Web.Areas.Admin.ViewModels
{
    public class BlogPostCategoryViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Slug { get; set; }

        public string Description { get; set; }

        public string PostAction { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<BlogPostCategory> BlogPostCategories { get; set; }
    }
}
