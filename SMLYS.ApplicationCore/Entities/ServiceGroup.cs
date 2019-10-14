using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Entities
{
    public partial class ServiceGroup : BaseEntity
    {
        public ServiceGroup()
        {
            Items = new HashSet<Item>();
        }

        public string Name { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
