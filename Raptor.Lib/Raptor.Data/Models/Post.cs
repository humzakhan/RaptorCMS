using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
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
        public int PostCategoryId { get; set; }

        public virtual PostCategory PostCategory { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
