using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Raptor.Data.Models.Blog;

namespace Raptor.Data.Models.Users
{
    public class Person
    {
        [Key]
        [ForeignKey("BusinessEntity")]
        public int BusinessEntityId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(180)]
        public string DisplayName { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        public string About { get; set; }

        public string Website { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsEmailVerified { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public DateTime DateModifiedUtc { get; set; }

        public DateTime DateLastLoginUtc { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public virtual ICollection<PersonRole> UserRoles { get; set; }

        public virtual ICollection<BlogComment> BlogComments { get; set; }

        public virtual Password Password { get; set; }

        public bool IsInRole(string systemKeyword) {
            return UserRoles.Any(u => u.Role.SystemKeyword == systemKeyword);
        }

        public bool IsInRole(int roleId) {
            return UserRoles.Any(u => u.RoleId == roleId);
        }
    }
}
