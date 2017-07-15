using System;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(152)]
        public string DisplayName { get; set; }

        [StringLength(15)]
        public string Birthday { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        public string About { get; set; }
        public DateTime DateModified { get; set; }
    }
}