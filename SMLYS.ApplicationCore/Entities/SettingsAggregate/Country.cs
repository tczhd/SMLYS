using SMLYS.ApplicationCore.Entities.CommonAggregate;

using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.SettingsAggregate
{
    public partial class Country : BaseEntity
    {
        public Country()
        {
            Address = new HashSet<Address>();
        }
        public string Name { get; set; }
        public string Iso2 { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
