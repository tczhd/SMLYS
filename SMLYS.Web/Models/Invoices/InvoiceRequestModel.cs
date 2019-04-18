using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Invoices
{
    [DataContract(Name = "invoice")]
    public class InvoiceRequestModel
    {
        [DataMember(Name = "patient_id")]
        public int PatientId { get; set; }
        [DataMember(Name = "doctor_id")]
        public int DoctorId { get; set; }
        [DataMember(Name = "invoice_date")]
        public string InvoiceDate { get; set; }
        [DataMember(Name = "invoice_items")]
        public List<InvoiceItemRequestModel> InvoiceItems { get; set; }
    }
}
