using SMLYS.ApplicationCore.DTOs.Payment;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;
using SMLYS.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Stripe
{
    public class StripeBasicRequestModel : BasicRequestModel
    {
        public CurrencyType Currency { get; set; }
        public bool Test { get; set; }

        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string OrderNumber { get; set; }
        public string TokenId { get; set; }
        public string CardF4L4 { get; set; }
        public string Description { get; set; }
        public StripeCreditCardRequestModel CreditCard { get; set; }

        public static implicit operator StripeBasicRequestModel(PaymentDataModel source)
        {
            return new StripeBasicRequestModel
            {
                Amount = source.PaymentAmount,
                Currency = source.CurrencyType,
                Description =source.Note,
                
                TokenId = source.TokenId,
                CreditCard = source.CreditCard != null ? new StripeCreditCardRequestModel
                {
                    cardCVV = source.CreditCard.cardCVV,
                    ExpiryYear = source.CreditCard.cardExpiryYear,
                    ExpiryMonth = source.CreditCard.cardExpiryMonth,
                    cardHolderAddress = source.CreditCard.cardHolderAddress,
                    CardHolderName = source.CreditCard.CardHolderName,
                    cardHolderPostalCode = source.CreditCard.cardHolderPostalCode,
                    cardNumber = source.CreditCard.cardNumber
                } : null
            };
        }
    }
}
