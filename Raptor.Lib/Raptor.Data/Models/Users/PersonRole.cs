using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raptor.Data.Models.Users
{
    public class PersonRole
    {
        [Key]
        [Column(Order = 1)]
        public int RoleId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Person")]
        public int BusinessEntityId { get; set; }
        public Guid RowGuid { get; set; }

        public virtual Role Role { get; set; }
        public virtual Person Person { get; set; }
    }
}
