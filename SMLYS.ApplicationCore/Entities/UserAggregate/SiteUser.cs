using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMLYS.ApplicationCore.Entities.UserAggregate
{
    public partial class SiteUser : BaseEntity
    {
        public SiteUser()
        {
            AddressCreatedByNavigation = new HashSet<Address>();
            AddressUpdatedByNavigation = new HashSet<Address>();
            ClinicCreatedByNavigation = new HashSet<Clinic>();
            ClinicUpdatedByNavigation = new HashSet<Clinic>();
            DoctorCreatedByNavigation = new HashSet<Doctor>();
            DoctorUpdatedByNavigation = new HashSet<Doctor>();
            InvoiceCreatedByNavigation = new HashSet<Invoice>();
            InvoiceUpdatedByNavigation = new HashSet<Invoice>();
            Item = new HashSet<Item>();
            PatientCreatedByNavigation = new HashSet<Patient>();
            PatientUpdatedByNavigation = new HashSet<Patient>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        public int SiteUserLevelId { get; set; }
        public string Active { get; set; }
        public string Note { get; set; }
        public int ClinicId { get; set; }
        public int? DoctorId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual SiteUserLevel SiteUserLevel { get; set; }
        public virtual ICollection<Address> AddressCreatedByNavigation { get; set; }
        public virtual ICollection<Address> AddressUpdatedByNavigation { get; set; }
        public virtual ICollection<Clinic> ClinicCreatedByNavigation { get; set; }
        public virtual ICollection<Clinic> ClinicUpdatedByNavigation { get; set; }
        public virtual ICollection<Doctor> DoctorCreatedByNavigation { get; set; }
        public virtual ICollection<Doctor> DoctorUpdatedByNavigation { get; set; }
        public virtual ICollection<Invoice> InvoiceCreatedByNavigation { get; set; }
        public virtual ICollection<Invoice> InvoiceUpdatedByNavigation { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual ICollection<Patient> PatientCreatedByNavigation { get; set; }
        public virtual ICollection<Patient> PatientUpdatedByNavigation { get; set; }
    }
}
