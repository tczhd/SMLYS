using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Invoices
{
    [DataContract(Name = "credit_card")]
    public class InoviceCreditCardRequestModel
    {
        [DataMember(Name = "card_holder_name")]
        public string CardHolderName { get; set; }
        [DataMember(Name = "card_number")]
        public string CardNumber { get; set; }
        [DataMember(Name = "card_month")]
        public int CardMonth { get; set; }
        [DataMember(Name = "card_year")]
        public int CardYear { get; set; }
        [DataMember(Name = "card_cvv")]
        public string CardCvv { get; set; }
        [DataMember(Name = "card_zip")]
        public string CardZip { get; set; }
        [DataMember(Name = "card_address")]
        public string CardAddress { get; set; }
    }
}