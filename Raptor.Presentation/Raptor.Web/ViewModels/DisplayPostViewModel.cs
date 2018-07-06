using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raptor.Data.Models.Blog;

namespace Raptor.Web.ViewModels
{
    public class DisplayPostViewModel
    {
        public BlogPost BlogPost { get; set; }

        public string PostCategoryName { get; set; }

        public List<BlogPost> RecentPosts { get; set; }

        public List<BlogPostCategory> Categories { get; set; }
    }
}
