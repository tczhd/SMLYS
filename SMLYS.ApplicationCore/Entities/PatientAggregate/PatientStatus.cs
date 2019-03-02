

using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.PatientAggregate
{
    public partial class PatientStatus : BaseEntity
    {
        public PatientStatus()
        {
            Patient = new HashSet<Patient>();
        }
        public string Status { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
