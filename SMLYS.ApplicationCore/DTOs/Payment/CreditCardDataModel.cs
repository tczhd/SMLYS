using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Payment
{
    public class CreditCardDataModel
    {
        public string CardHolderName { get; set; }
        public string cardNumber { get; set; }
        public int cardExpiryMonth { get; set; }
        public int cardExpiryYear { get; set; }
        public string cardCVV { get; set; }
        public string cardHolderAddress { get; set; }
        public string cardHolderPostalCode { get; set; }
        public string GetCardF4L4 (){
            if (!string.IsNullOrWhiteSpace(cardNumber) && cardNumber.Length == 16)
            {
                return cardNumber.Substring(0, 4) + cardNumber.Substring(cardNumber.Length - 4, 4);
            }

            return string.Empty;
        }

        public string GetCardL4()
        {
            if (!string.IsNullOrWhiteSpace(cardNumber) && cardNumber.Length == 16)
            {
                return cardNumber.Substring(cardNumber.Length - 4, 4);
            }

            return string.Empty;
        }
    }
}
