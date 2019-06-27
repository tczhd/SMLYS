using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Stripe
{
    public class StripeCreditCardRequestModel
    {
        public string CardHolderName { get; set; }
        public string cardNumber { get; set; }
        public long? ExpiryYear { get; set; }
        public long? ExpiryMonth { get; set; }
        public string cardCVV { get; set; }
        public string cardHolderAddress { get; set; }
        public string cardHolderPostalCode { get; set; }
    }
}
