
using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.CommonAggregate
{
    public partial class AddressType
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string AddressType1 { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
