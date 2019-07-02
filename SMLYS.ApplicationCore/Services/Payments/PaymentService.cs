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
using SMLYS.ApplicationCore.Entities.PatientAggregate;

namespace SMLYS.ApplicationCore.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<InvoicePayment> _invoicePaymentRepository;
        private readonly IRepository<PatientCardOnFile> _patientCardOnFileRepository;
        private readonly IThirdPartyPaymentService _stripePaymentService;
        private readonly UserHandler _userHandler;
        private int _clinicId;

        public PaymentService(IRepository<Invoice> invoiceRepository,
            IThirdPartyPaymentService stripePaymentService,
            IRepository<InvoicePayment> invoicePaymentRepository,
             IRepository<PatientCardOnFile> patientCardOnFileRepository,
            UserHandler userHandler)
        {
            _invoiceRepository = invoiceRepository;
            _stripePaymentService = stripePaymentService;
            _invoicePaymentRepository = invoicePaymentRepository;
            _patientCardOnFileRepository = patientCardOnFileRepository;
            _userHandler = userHandler;
            _clinicId = _userHandler.GetUserContext().ClinicId;
        }

        public PaymentResultModel ApplyPayment(PaymentDataModel requestMdoel)
        {
            var result = new PaymentResultModel();

            var userContext = _userHandler.GetUserContext();
            var invoiceDetailSpecification = new InvoiceSpecification(_clinicId);
            invoiceDetailSpecification.AddInvoiceId(requestMdoel.InvoiceId);
            var invoice = _invoiceRepository.GetSingleBySpec(invoiceDetailSpecification);

            if (invoice != null)
            {
                var payementResult = _stripePaymentService.ProcessPayment((StripeBasicRequestModel)requestMdoel);
                result = payementResult;
                var cardLast4 = requestMdoel.CreditCard.GetCardF4L4();
                result.CardLast4 = cardLast4;

                if (payementResult.Success && payementResult.Approved)
                {
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
                        Amount = requestMdoel.PaymentAmount,
                        TransactionId = payementResult.TransactionId,
                        AuthorizationCode = payementResult.AuthCode,
                        CardToken = payementResult.CardToken,
                        CardF4L4 = cardLast4
                    };

                    var invoicePayment = new InvoicePayment
                    {
                        AmountPaid = requestMdoel.PaymentAmount,
                        InvoiceId = requestMdoel.InvoiceId,
                        Payment = payment,
                        Note = requestMdoel.Note
                    };

                    _invoicePaymentRepository.AddOnly(invoicePayment);

                    var patientCardOnFile = new PatientCardOnFile {
                        Active = true,
                        CardF4L4 = requestMdoel.CreditCard.GetCardF4L4(),
                        CardToken = payementResult.CardToken,
                        UpdatedDateUtc = DateTime.UtcNow,
                        PatientId = invoice.PatientId,
                        UpdatedBy = userContext.SiteUserId
                    };

                    _patientCardOnFileRepository.AddOnly(patientCardOnFile);

                    invoice.AmountPaid += requestMdoel.PaymentAmount;

                    _invoicePaymentRepository.SaveAll();

                    result.AmountPaidTotal = invoice.AmountPaid;
                }
                else {
                    result.Message = "Process payment failed. ";
                }
            }
            else
            {
                result.Message = "Invalid invoice Id, Please choose right one and try again. ";
            }

            return result;
        }

        public PaymentResultModel Void(PaymentDataModel requestMdoel)
        {
            throw new NotImplementedException();
        }
    }
}
