

using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.PatientAggregate
{
    public partial class Family : BaseEntity
    {
        public Family()
        {
            Patient = new HashSet<Patient>();
        }

        public string Name { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
