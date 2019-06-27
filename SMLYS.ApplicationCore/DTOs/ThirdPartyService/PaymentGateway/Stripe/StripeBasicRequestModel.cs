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
    }
}
