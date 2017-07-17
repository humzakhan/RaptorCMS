using System;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Users
{
    public class PhoneNumber
    {
        [Key]
        public int PhoneNumberId { get; set; }
        public string Number { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public bool IsDefault { get; set; }
        public int PersonId { get; set; }
        public DateTime DateCreatedUtc { get; set; }
        public DateTime DateModifiedUtc { get; set; }

        public PhoneNumberType PhoneNumberType {
            get { return (PhoneNumberType)PhoneNumberTypeId; }
            set { PhoneNumberTypeId = (int)value; }
        }

        public virtual Person Person { get; set; }
    }
}
