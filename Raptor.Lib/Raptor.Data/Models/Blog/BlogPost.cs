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

        public DateTime DateCreatedUtc { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public string Excerpt { get; set; }

        public PostStatus Status { get; set; }

        public bool IsCommentsAllowed { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public DateTime DateModifiedUtc { get; set; }

        public Guid Guid { get; set; }

        [StringLength(255)]
        public string Link { get; set; }

        public PostType PostType { get; set; }

        public int CommentsCount { get; set; }

        [ForeignKey("BlogPostCategory")]
        public int PostCategoryId { get; set; }

        [ForeignKey("Person")]
        public int CreatedById { get; set; }

        public virtual BlogPostCategory BlogPostCategory { get; set; }

        public virtual ICollection<BlogComment> Comments { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }

        public virtual Person CreatedBy { get; set; }
    }
}
