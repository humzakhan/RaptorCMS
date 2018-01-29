using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raptor.Data.Models.Users
{
    public class ForgotPasswordRequest
    {
        [Key]
        public int ForgotPasswordId { get; set; }
        public string Link { get; set; }

        [ForeignKey(nameof(Person))]
        public int BusinessEntityId { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public Person Person { get; set; }
    }
}
