
using SMLYS.ApplicationCore.DTOs.Invoices;
using System.Collections.Generic;


namespace SMLYS.ApplicationCore.Interfaces.Services.Invoices
{
    public interface IInvoiceService
    {
        InvoiceModel CreateInvoiceAsync(InvoiceModel invoice);

        List<InvoiceModel> SearchInvoices();

        InvoiceModel SearchInvoice(int invoiceId);
    }
}
