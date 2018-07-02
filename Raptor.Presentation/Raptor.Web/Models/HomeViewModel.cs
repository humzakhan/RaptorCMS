using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raptor.Data.Models.Blog;

namespace Raptor.Web.Models
{
    public class HomeViewModel
    {
        public List<BlogPost> MostRecentPosts { get; set; }
    }
}
