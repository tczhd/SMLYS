using SMLYS.ApplicationCore.Entities.CommonAggregate;

using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.SettingsAggregate
{
    public partial class Region : BaseEntity
    {
        public Region()
        {
            Address = new HashSet<Address>();
        }

        public string Name { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
