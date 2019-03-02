
using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.CommonAggregate
{
    public partial class AddressType : BaseEntity
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public string AddressType1 { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
