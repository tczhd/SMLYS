using SMLYS.ApplicationCore.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Payments
{
    [DataContract(Name = "result_model")]
    public class PaymentResultApiModel : ResultModel
    {
        [DataMember(Name = "approved")]
        public bool Approved { get; set; }
        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }
        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }
        [DataMember(Name = "card_last4")]
        public string CardLast4 { get; set; }
        [DataMember(Name = "amount_paid")]
        public decimal AmountPaid { get; set; }
        [DataMember(Name = "amount_paid_total")]
        public decimal AmountPaidTotal { get; set; }
        [DataMember(Name = "failure_code")]
        public string FailureCode { get; set; }
        [DataMember(Name = "failure_message")]
        public string FailureMessage { get; set; }

        public static implicit operator PaymentResultApiModel(PaymentResultModel source)
        {
            return new PaymentResultApiModel
            {
                AmountPaid = source.AmountPaid,
                Approved = source.Approved,
                AuthCode = source.AuthCode,
                CardLast4 = source.CardLast4,
                AmountPaidTotal = source.AmountPaidTotal,
                FailureCode = source.FailureCode,
                FailureMessage = source.FailureMessage,
                Message = source.Message,
                Success = source.Success,
                TransactionId = source.TransactionId
            };
        }
    }
}
