using System;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }

        [StringLength(50)]
        public string Author { get; set; }

        [StringLength(100)]
        public string AuthorEmail { get; set; }

        [StringLength(200)]
        public string AuthorUrl { get; set; }

        [StringLength(100)]
        public string AuthorIp { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateCreatedGmt { get; set; }
        public string Content { get; set; }
        public int Karma { get; set; }
        public bool Approved { get; set; }

        [StringLength(255)]
        public string Agent { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
