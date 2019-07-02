using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.Services.Payment
{
    public interface IPaymentService
    {
        PaymentResultModel ApplyPayment(PaymentDataModel requestMdoel);
        PaymentResultModel Void(PaymentDataModel requestMdoel);
    }
}
