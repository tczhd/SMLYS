using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common
{
    public class PaymentResult : Result
    {
        public PaymentGateWayType PaymentGateWay { get; set; }
    }
}
