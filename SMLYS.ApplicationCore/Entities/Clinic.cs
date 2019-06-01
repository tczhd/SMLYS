using System;
using System.Collections.Generic;
using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Entities.UserAggregate;


namespace SMLYS.ApplicationCore.Entities
{
    public partial class Clinic : BaseEntity
    {
        public Clinic()
        {
            Doctor = new HashSet<Doctor>();
            Invoice = new HashSet<Invoice>();
            Item = new HashSet<Item>();
            Patient = new HashSet<Patient>();
            SiteUser = new HashSet<SiteUser>();
            Payment = new HashSet<Payment>();
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string Note { get; set; }
        
        public int AddressId { get; set; }

        public virtual SiteUser CreatedByNavigation { get; set; }
        public virtual SiteUser UpdatedByNavigation { get; set; }
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
        public virtual ICollection<SiteUser> SiteUser { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual Address Address { get; set; }
    }
}
