using Raptor.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raptor.Data.Models.Blog
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCreatedGmt { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public PostStatus Status { get; set; }
        public string CommentsStatus { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateModifiedGmt { get; set; }
        public Guid Guid { get; set; }

        [StringLength(255)]
        public string Link { get; set; }
        public PostType PostType { get; set; }
        public int CommentsCount { get; set; }
        public int BlogPostCategoryId { get; set; }

        [ForeignKey("BusinessEntity")]
        public int CreatedById { get; set; }

        public virtual BlogPostCategory BlogPostCategory { get; set; }
        public virtual ICollection<BlogComment> Comments { get; set; }
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
