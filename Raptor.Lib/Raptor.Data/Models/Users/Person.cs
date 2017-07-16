using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime DateCreatedUtc { get; set; }
        public DateTime DateModifiedUtc { get; set; }
        public DateTime DateLastLoginUtc { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
