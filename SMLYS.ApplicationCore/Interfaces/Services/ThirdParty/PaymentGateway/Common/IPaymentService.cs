using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.Services.ThirdParty.PaymentGateway.Common
{
    public interface IPaymentService
    {
        Result ProcessPayment(BasicRequestModel requestMdoel);
        Result ProcessVoid(BasicRequestModel requestMdoel);

    }
}
