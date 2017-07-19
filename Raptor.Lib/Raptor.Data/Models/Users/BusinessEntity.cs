using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Users
{
    public class BusinessEntity
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public Guid RowGuid { get; set; }

        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
        public virtual Person Person { get; set; }
    }
}
