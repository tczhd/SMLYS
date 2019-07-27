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
            _clinicId = _userHandler.GetUserContext() != null? _userHandler.GetUserContext().ClinicId: 0;
        }
        public InvoiceModel CreateInvoiceAsync(InvoiceModel invoiceModel)
        {
            try
            {
                var invoice = (Invoice)invoiceModel;

                _invoiceRepository.AddOnly(invoice);

                _invoiceRepository.SaveAll();

                invoiceModel.InvoiceId = invoice.Id;
                invoiceModel.DisplayId = invoice.DisplayId;

                return invoiceModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Add Invoice failed: " + ex.Message);
            }        
        }

        public InvoiceModel SearchInvoice(int invoiceId)
        {
            var invoiceDetailSpecification = new InvoiceSpecification(_clinicId);
            invoiceDetailSpecification.AddDisplayId(invoiceId);
            var data = _invoiceRepository.GetSingleBySpec(invoiceDetailSpecification);

            return data !=null?data:null;
        }

        public int SearchInvoiceCount(InvoiceSearchDataModel searchModel)
        {
            return  _invoiceRepository.Count(GetInvoiceSpecification(searchModel, true));
        }

        public List<InvoiceModel> SearchInvoices(InvoiceSearchDataModel searchModel)
        {
            var data = _invoiceRepository.List(GetInvoiceSpecification(searchModel, false));
            var result = data.Select(p => (InvoiceModel)p).ToList();

            return result;
        }

        private InvoiceSpecification GetInvoiceSpecification(InvoiceSearchDataModel searchModel, bool IsCount)
        {
            var invoiceSpecification = new InvoiceSpecification(_clinicId);
            if (searchModel.DisplayId != null && searchModel.DisplayId > 0)
            {
                invoiceSpecification.AddDisplayId((int)searchModel.DisplayId);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.FirstName))
            {
                invoiceSpecification.AddFirstName(searchModel.FirstName);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.LastName))
            {
                invoiceSpecification.AddFirstName(searchModel.LastName);
            }
            if (searchModel.InvoiceFromDate != null)
            {
                invoiceSpecification.AddFromDate((DateTime)searchModel.InvoiceFromDate);
            }
            if (searchModel.InvoiceToDate != null)
            {
                invoiceSpecification.AddToDate((DateTime)searchModel.InvoiceToDate);
            }

            if (!IsCount)
            {
                invoiceSpecification.AddPagination(searchModel.CurrentPage, searchModel.PageSize);
            }

            return invoiceSpecification;
        }
    }
}
