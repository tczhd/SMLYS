using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Payment
{
    public class CreditCardDataModel
    {
        public string CardHolderName { get; set; }
        public string cardNumber { get; set; }
        public DateTime cardExpiry { get; set; }
        public string cardCVV { get; set; }
        public string cardHolderAddress { get; set; }
        public string cardHolderPostalCode { get; set; }
    }
}
