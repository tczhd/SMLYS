using SMLYS.ApplicationCore.DTOs.Invoices;
using SMLYS.Web.ViewModels.Adresses;
using SMLYS.Web.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Invoices
{
    public class InvoiceViewModel
    {
        [Display(Name = "Invoice Number")]
        public int InvoiceId { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }
        [Display(Name = "Doctor Id")]
        public int DoctorId { get; set; }
        [Display(Name = "Clinic Id")]
        public int ClinicId { get; set; }
        [Display(Name = "Family Id")]
        public int FamilyId { get; set; }
        [Required]
        [Display(Name = "Send To")]
        public string SendToName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "address")]
        public string Address { get; set; }
        [Display(Name = "Physician")]
        public string DoctorName { get; set; }
        [Display(Name = "Re-occouring")]
        public bool ReOccouring { get; set; }
        [Display(Name = " ReOccouring Start Date")]
        public DateTime? ReOccouringStartDate { get; set; }

        [Display(Name = "Re-occouring Type")]
        public int ReOccouringType { get; set; }

        [Display(Name = "SubTotal")]
        public decimal SubTotal { get; set; }
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
        [Display(Name = "Tax")]
        public decimal Tax { get; set; }
        [Display(Name = "Total")]
        public decimal Total { get; set; }
        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }
        [Display(Name = "Balance Due")]
        public decimal BalanceDue { get; set; }
        [Display(Name = "Patient")]
        public AddressViewModel PatientAddress { get; set; }
        [Display(Name = "Patients")]
        public List<PatientViewModel> Patients { get; set; }
        [Display(Name = "Items")]
        public List<InvoiceItemViewModel> InvoiceItems { get; set; }

        public static implicit operator InvoiceViewModel(InvoiceModel source)
        {
            return new InvoiceViewModel
            {
                AmountPaid = source.AmountPaid,
                ClinicId = source.ClinicId,
                DoctorId = source.DoctorId,
                DoctorName = source.DoctorName,
                InvoiceDate = source.InvoiceDate,
                InvoiceId = source.InvoiceId,
                PatientId = source.PatientId,
                PatientName = source.PatientName,
                ReOccouring = source.ReOccouring,
                Total = source.Total,
                SubTotal =source.Subtotal,
                Tax = source.TaxTotal,
                BalanceDue = source.Total - source.AmountPaid,
                PatientAddress = source.PatientAddress,
                InvoiceItems = source.InvoiceItems.Select(p => (InvoiceItemViewModel)p).ToList()
            };
        }
    }
}
