using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Entities
{
    public partial class Payment : BaseEntity
    {
        public Payment() {
            InvoicePayment = new HashSet<InvoicePayment>();
        }

        public int PaymentTypeId { get; set; }
        public int PaymentMethodTypeId { get; set; }
        public int PaymentStatusTypeId { get; set; }
        public decimal Amount { get; set; }
        public string AuthorizationCode { get; set; }
        public string TransactionId { get; set; }
        public string Description { get; set; }
        public string CardToken { get; set; }
        public string CardF4L4 { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public int UpdatedBy { get; set; }
        public int ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }
        public virtual SiteUser UpdatedByNavigation { get; set; }

        public virtual ICollection<InvoicePayment> InvoicePayment { get; set; }
    }
}
