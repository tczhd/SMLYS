using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Payment;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.Services.ThirdParty.PaymentGateway.Common
{
    public interface IThirdPartyPaymentService
    {
        PaymentResultModel ProcessPayment(BasicRequestModel requestModel);
        PaymentResultModel ProcessVoid(BasicRequestModel requestModel);
        PaymentResultModel ProcessRefund(BasicRequestModel requestModel);
    }
}
