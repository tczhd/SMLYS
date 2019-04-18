using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Invoices
{
    [DataContract(Name = "invoice_item")]
    public class InvoiceItemRequestModel
    {
        [DataMember(Name = "item_id")]
        public int ItemId { get; set; }
        [DataMember(Name = "quantity")]
        public int Quantity{ get; set; }
        [DataMember(Name = "cost")]
        public decimal Cost { get; set; }
    }
}
