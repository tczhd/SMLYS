
using SMLYS.ApplicationCore.DTOs.Invoices;
using System.Collections.Generic;


namespace SMLYS.ApplicationCore.Interfaces.Services.Invoices
{
    public interface IInvoiceService
    {
        List<InvoiceModel> CreateInvoiceAsync(List<InvoiceModel> invoice);
    }
}
