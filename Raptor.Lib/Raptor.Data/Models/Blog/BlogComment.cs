using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Raptor.Data.Models.Users;

namespace Raptor.Data.Models.Blog
{
    public class BlogComment
    {
        [Key]
        public int CommentId { get; set; }
        
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        [ForeignKey(nameof(Person))]
        public int BusinessEntityId { get; set; }

        [StringLength(100)]
        public string AuthorIp { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateCreatedGmt { get; set; }

        public string Content { get; set; }

        public int Karma { get; set; }

        public bool Approved { get; set; }

        [StringLength(255)]
        public string Agent { get; set; }

        public BlogPost Post { get; set; }

        public Person Person { get; set; }
    }
}
