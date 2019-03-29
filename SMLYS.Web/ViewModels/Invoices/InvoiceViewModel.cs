﻿using System;
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
        [Required]
        [Display(Name = "Send To")]
        public string SendToName { get; set; }
        [Required]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Address")]
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
        [Display(Name = "Items")]
        public List<InvoiceItemViewModel> Items { get; set; }
    }
}