using SMLYS.ApplicationCore.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Payment
{
    public class PaymentResultModel: Result
    {
        public bool Approved { get; set; }
        public string TransactionId { get; set; }
        public string AuthCode { get; set; }
        public string CardToken { get; set; }
        public string FailureCode { get; set; }
        public string FailureMessage { get; set; }
    }
}
