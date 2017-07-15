using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public CustomerType CustomerType { get; set; }
    }
}
