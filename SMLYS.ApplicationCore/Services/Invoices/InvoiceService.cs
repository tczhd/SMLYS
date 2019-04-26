using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Invoices;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using SMLYS.ApplicationCore.Specifications.Invoices;

namespace SMLYS.ApplicationCore.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly UserHandler _userHandler;
        private int _clinicId;

        public InvoiceService(IRepository<Invoice> invoiceRepository, UserHandler userHandler)
        {
            _invoiceRepository = invoiceRepository;
            _userHandler = userHandler;
            _clinicId = _userHandler.GetUserContext().ClinicId;
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

        public InvoiceModel SearchInvoice(int invoiceId)
        {
            var invoiceDetailSpecification = new InvoiceSpecification(_clinicId);
            invoiceDetailSpecification.AddInvoiceId(invoiceId);
            var data = _invoiceRepository.GetSingleBySpec(invoiceDetailSpecification);

            return data;
        }

        public List<InvoiceModel> SearchInvoices()
        {
            var invoiceSpecification = new InvoiceSpecification(_clinicId);
            var data = _invoiceRepository.List(invoiceSpecification);
            var result = data.Select(p => (InvoiceModel)p).ToList();

            return result;
        }
    }
}
