using System;

namespace SMLYS.ApplicationCore.Entities.InvoiceAggregate
{
    public partial class InvoiceReOccouring : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int InvoiceReOccouringTypeId { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public bool Active { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual InvoiceReOccouringType InvoiceReOccouringType { get; set; }
    }
}
