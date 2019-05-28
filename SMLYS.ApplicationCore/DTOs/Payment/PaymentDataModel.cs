using SMLYS.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Payment
{
    public class PaymentDataModel
    {
        public PaymentType PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string CheckNo { get; set; }
        public CreditCardDataModel CreditCard { get; set; }
    }
}
