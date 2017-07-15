using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raptor.Data.Models
{
    public class UserRole
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; }

        [Column(Order = 2)]
        public int UserId { get; set; }
        public Guid rowguid { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
