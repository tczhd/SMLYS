using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Invoices
{
    public class InvoiceSearchDataModel
    {
        public int? InvoiceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? InvoiceFromDate { get; set; }
        public DateTime? InvoiceToDate { get; set; }
    }
}
