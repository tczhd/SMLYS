using SMLYS.ApplicationCore.DTOs.Payment;
using System.Runtime.Serialization;
using SMLYS.ApplicationCore.Enums;

namespace SMLYS.Web.Models.Invoices
{
    [DataContract(Name = "invoice_payment")]
    public class InvoicePaymentRequestModel
    {
        [DataMember(Name = "invoice_id")]
        public int InvoiceId { get; set; }
        [DataMember(Name = "amount_paid")]
        public decimal AmountPaid { get; set; }
        [DataMember(Name = "payment_type_id")]
        public int PaymentTypeId { get; set; }
        [DataMember(Name = "check_no")]
        public string CheckNo { get; set; }
        [DataMember(Name = "token_id")]
        public string TokenId { get; set; }
        [DataMember(Name = "credit_card")]
        public InoviceCreditCardRequestModel CreditCard { get; set; }

        public static implicit operator PaymentDataModel(InvoicePaymentRequestModel source)
        {
            return new PaymentDataModel
            {
                CheckNo = source.CheckNo,
                CurrencyType = ApplicationCore.Enums.CurrencyType.CAD,
                InvoiceId = source.InvoiceId,
                PaymentAmount = source.AmountPaid,
                PaymentGateWay = PaymentGateWayType.Stripe,
                PaymentStatusType = PaymentStatusType.Purchase,
                PaymentType = (PaymentType)source.PaymentTypeId,
                TokenId = source.TokenId,
                CreditCard = source.CreditCard != null ? new CreditCardDataModel {
                    cardCVV = source.CreditCard.CardCvv,
                    cardExpiryYear = source.CreditCard.CardYear,
                    cardExpiryMonth = source.CreditCard.CardMonth,
                    cardHolderAddress = source.CreditCard.CardAddress,
                    CardHolderName = source.CreditCard.CardHolderName,
                    cardHolderPostalCode = source.CreditCard.CardZip,
                    cardNumber = source.CreditCard.CardNumber
                } : null
            };
        }
    }
}
