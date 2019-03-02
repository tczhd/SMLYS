using System;
using System.Collections.Generic;
using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Entities.UserAggregate;


namespace SMLYS.ApplicationCore.Entities.InvoiceAggregate
{
    public partial class Invoice : BaseEntity
    {
        public Invoice()
        {
            InvoiceItem = new HashSet<InvoiceItem>();
            InvoiceReOccouring = new HashSet<InvoiceReOccouring>();
        }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public decimal AmountPaid { get; set; }
        public int InvoiceStatus { get; set; }
        public int PaymentStatus { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public int? UpdatedBy { get; set; }
        public bool ReOccouring { get; set; }
        public int ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }
        public virtual SiteUser CreatedByNavigation { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual SiteUser UpdatedByNavigation { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }
        public virtual ICollection<InvoiceReOccouring> InvoiceReOccouring { get; set; }
    }
}
