using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Payment;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Payment;
using SMLYS.ApplicationCore.Specifications.Invoices;
using System;
using SMLYS.ApplicationCore.Entities;
using SMLYS.ApplicationCore.Enums;
using SMLYS.ApplicationCore.Interfaces.Services.ThirdParty.PaymentGateway.Common;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Stripe;

namespace SMLYS.ApplicationCore.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IThirdPartyPaymentService _stripePaymentService;
        private readonly UserHandler _userHandler;
        private int _clinicId;

        public PaymentService(IRepository<Invoice> invoiceRepository,
            IThirdPartyPaymentService stripePaymentService, UserHandler userHandler)
        {
            _invoiceRepository = invoiceRepository;
            _stripePaymentService = stripePaymentService;
            _userHandler = userHandler;
            _clinicId = _userHandler.GetUserContext().ClinicId;
        }

        public Result ApplyPayment(PaymentDataModel requestMdoel)
        {
            Result result = new Result();

            var userContext = _userHandler.GetUserContext();
            var invoiceDetailSpecification = new InvoiceSpecification(_clinicId);
            invoiceDetailSpecification.AddInvoiceId(requestMdoel.InvoiceId);
            var data = _invoiceRepository.GetSingleBySpec(invoiceDetailSpecification);

            var payementResult = _stripePaymentService.ProcessPayment((StripeBasicRequestModel)requestMdoel);

            var payment = new Payment
            {
                ClinicId = _clinicId,
                Description = requestMdoel.Note,
                PaymentDate = DateTime.UtcNow,
                PaymentMethodTypeId = (int)PaymentMethodType.Visa,
                PaymentStatusTypeId = (int)requestMdoel.PaymentStatusType,
                PaymentTypeId = (int)requestMdoel.PaymentType,
                UpdatedBy = userContext.SiteUserId,
                UpdatedDateUtc = DateTime.UtcNow,
                Amount = requestMdoel.PaymentAmount
            };

            var invoicePayment = new InvoicePayment {
                 AmountPaid = requestMdoel.PaymentAmount,
                 InvoiceId = requestMdoel.InvoiceId,
                 Payment = payment,
                 Note = requestMdoel.Note
            };
           
            return result;
        }

        public Result Void(PaymentDataModel requestMdoel)
        {
            throw new NotImplementedException();
        }
    }
}
