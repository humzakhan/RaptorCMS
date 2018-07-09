using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raptor.Web.ViewModels
{
    public class CommentViewModel
    {
        public int BlogPostId { get; set; }
        
        public string CommentContent { get; set; }
    }
}
