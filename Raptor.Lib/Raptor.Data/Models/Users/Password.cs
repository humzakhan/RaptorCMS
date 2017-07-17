using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raptor.Data.Models.Users
{
    public class Password
    {
        [Key]
        [ForeignKey("Person")]
        public int BusinessEntityId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid RowGuid { get; set; }

        public virtual Person Person { get; set; }
    }
}
