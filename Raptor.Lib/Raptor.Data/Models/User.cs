using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsDeleted { get; set; }
        public int UserProfileId { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }


        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
