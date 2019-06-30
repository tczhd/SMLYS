using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Payment;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Payment;
using SMLYS.ApplicationCore.Specifications.Invoices;
using System;
using SMLYS.ApplicationCore.Entities;

namespace SMLYS.ApplicationCore.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly UserHandler _userHandler;
        private int _clinicId;

        public PaymentService(IRepository<Invoice> invoiceRepository, UserHandler userHandler)
        {
            _invoiceRepository = invoiceRepository;
            _userHandler = userHandler;
            _clinicId = _userHandler.GetUserContext().ClinicId;
        }

        public Result ApplyPayment(PaymentDataModel requestMdoel)
        {
            Result result = new Result();
            var invoiceDetailSpecification = new InvoiceSpecification(_clinicId);
            invoiceDetailSpecification.AddInvoiceId(requestMdoel.InvoiceId);
            var data = _invoiceRepository.GetSingleBySpec(invoiceDetailSpecification);

            Payment payment = new Payment
            {
                ClinicId = _clinicId,
                Description = requestMdoel.Note,
                PaymentDate = DateTime.UtcNow,
               // PaymentMethodTypeId = requestMdoel.PaymentType
            };
            //data.InvoicePayment.Add
            return result;
        }

        public Result Void(PaymentDataModel requestMdoel)
        {
            throw new NotImplementedException();
        }
    }
}
