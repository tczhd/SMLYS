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
        public decimal Amount { get; set; }
    }
}
