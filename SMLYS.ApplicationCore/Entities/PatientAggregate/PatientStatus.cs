

using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.PatientAggregate
{
    public partial class PatientStatus
    {
        public PatientStatus()
        {
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
