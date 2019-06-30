using SMLYS.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Payment
{
    public class PaymentDataModel
    {
        public PaymentType PaymentType { get; set; }
        public PaymentGateWayType PaymentGateWay { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatusType PaymentStatusType { get; set; }
        public int InvoiceId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string CheckNo { get; set; }
        public string TokenId { get; set; }
        public string TransactionId { get; set; }
        public string Note { get; set; }
        public CreditCardDataModel CreditCard { get; set; }
    }
}
