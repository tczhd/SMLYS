using SMLYS.ApplicationCore.DTOs.Invoices;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMLYS.ApplicationCore.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> _invoiceRepository;

        public InvoiceService(IRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public List<InvoiceModel> CreateInvoiceAsync(List<InvoiceModel> invoiceModels)
        {
            try
            {
                var invoices = invoiceModels.Select(p => (Invoice)p).ToList();

                foreach (Invoice invoice in invoices)
                {
                    _invoiceRepository.AddOnly(invoice);
                }

                _invoiceRepository.SaveAll();

                return invoiceModels;
            }
            catch (Exception ex)
            {
                throw new Exception("Add Invoice failed: " + ex.Message);
            }        
        }
    }
}
