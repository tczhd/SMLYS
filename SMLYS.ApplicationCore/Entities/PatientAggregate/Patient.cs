using System;
using System.Collections.Generic;
using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Entities.UserAggregate;


namespace SMLYS.ApplicationCore.Entities.PatientAggregate
{
    public partial class Patient : BaseEntity
    {
        public Patient()
        {
            Invoice = new HashSet<Invoice>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Note { get; set; }
        public int FamilyId { get; set; }
        public int AddressId { get; set; }
        public int ClinicId { get; set; }
        public bool PrimaryMember { get; set; }
        public bool Minor { get; set; }
        public int DoctorId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual SiteUser CreatedByNavigation { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Family Family { get; set; }
        public virtual PatientStatus StatusNavigation { get; set; }
        public virtual SiteUser UpdatedByNavigation { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
