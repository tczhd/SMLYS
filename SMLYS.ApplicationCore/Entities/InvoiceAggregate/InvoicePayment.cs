using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Entities.InvoiceAggregate
{
    public partial class InvoicePayment : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int PaymentId { get; set; }
        public decimal AmountPaid { get; set; }
        public string Note { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
