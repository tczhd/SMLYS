using SMLYS.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Entities.PatientAggregate
{
    public partial class PatientCardOnFile : BaseEntity
    {
        public PatientCardOnFile()
        {
        }

        public int PatientId { get; set; }
        public string Name { get; set; }
        public string CustomerCode { get; set; }
        public string CardToken { get; set; }
        public string CardF4L4 { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public bool Active { get; set; }
        public int UpdatedBy { get; set; }
        public virtual Patient Patient{ get; set; }
        public virtual SiteUser UpdatedByNavigation { get; set; }
    }
}
