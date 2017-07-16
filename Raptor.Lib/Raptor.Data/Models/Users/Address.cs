using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Users
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateCreatedUtc { get; set; }
        public DateTime DateModifiedUtc { get; set; }


        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
    }
}
