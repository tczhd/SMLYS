
using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.InvoiceAggregate
{
    public partial class InvoiceReOccouringType
    {
        public InvoiceReOccouringType()
        {
            InvoiceReOccouring = new HashSet<InvoiceReOccouring>();
        }

        public int Id { get; set; }
        public string ReOccouringName { get; set; }

        public virtual ICollection<InvoiceReOccouring> InvoiceReOccouring { get; set; }
    }
}
