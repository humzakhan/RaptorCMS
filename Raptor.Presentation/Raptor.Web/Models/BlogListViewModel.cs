using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raptor.Data.Models.Blog;

namespace Raptor.Web.Models
{
    public class BlogListViewModel
    {
        public IList<BlogPost> RecentPosts { get; set; }

        public IList<BlogPost> AllPosts { get; set; }

        public IList<BlogPostCategory> Categories { get; set; }

        public string Category { get; set; }
    }
}
