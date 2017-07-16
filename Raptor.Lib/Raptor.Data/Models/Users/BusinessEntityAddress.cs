namespace Raptor.Data.Models.Users
{
    public class BusinessEntityAddress
    {
        public int BusinessEntityId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }
        public virtual Address Address { get; set; }

        public virtual AddressType AddressType {
            get { return (AddressType)AddressId; }
            set { AddressId = (int)value; }

        }
    }
}
