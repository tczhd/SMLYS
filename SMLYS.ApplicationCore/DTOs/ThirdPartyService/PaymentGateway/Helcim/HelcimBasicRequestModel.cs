using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Helcim
{
    public class HelcimBasicRequestModel : BasicRequestModel
    {
        public string AccountId { get; set; }
        public string ApiToken { get; set; }
        public string TransactionType { get; set; }
        public string TerminalId { get; set; }
        public bool Test { get; set; }

        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string OrderNumber { get; set; }
        public string CardToken { get; set; }
        public string CardF4L4 { get; set; }
        public string Comments { get; set; }
        public HelcimCreditCardRequestModel CreditCard { get; set; }
    }
}
