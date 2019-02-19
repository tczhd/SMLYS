using SMLYS.ApplicationCore.Entities.DoctorAggregate;

using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.SettingsAggregate
{
    public partial class Specality
    {
        public Specality()
        {
            DoctorSpecality = new HashSet<DoctorSpecality>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DoctorSpecality> DoctorSpecality { get; set; }
    }
}
