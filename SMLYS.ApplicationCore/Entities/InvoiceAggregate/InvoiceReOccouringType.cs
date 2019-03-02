
using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.InvoiceAggregate
{
    public partial class InvoiceReOccouringType : BaseEntity
    {
        public InvoiceReOccouringType()
        {
            InvoiceReOccouring = new HashSet<InvoiceReOccouring>();
        }

        public string ReOccouringName { get; set; }

        public virtual ICollection<InvoiceReOccouring> InvoiceReOccouring { get; set; }
    }
}
