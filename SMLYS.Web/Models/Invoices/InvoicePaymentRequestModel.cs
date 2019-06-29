using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Invoices
{
    [DataContract(Name = "invoice_payment")]
    public class InvoicePaymentRequestModel
    {
        [DataMember(Name = "invoice_id")]
        public int InvoiceId { get; set; }
        [DataMember(Name = "amount_paid")]
        public decimal AmountPaid { get; set; }
        [DataMember(Name = "payment_type_id")]
        public int PaymentTypeId { get; set; }
        [DataMember(Name = "credit_card")]
        public InoviceCreditCardRequestModel CreditCard { get; set; }
    }
}
