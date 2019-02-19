

using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.PatientAggregate
{
    public partial class Family
    {
        public Family()
        {
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
