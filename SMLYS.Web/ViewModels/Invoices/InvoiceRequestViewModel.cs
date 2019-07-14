using SMLYS.ApplicationCore.DTOs.Invoices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Invoices
{
    public class InvoiceRequestViewModel
    {
        public InvoiceRequestViewModel()
        {
            Invoices = new List<InvoiceViewModel>();
        }

        [Display(Name = "Invoice Number")]
        public int? InvoiceId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Invoice From Date")]
        public string InvoiceFromDate { get; set; }
        [Display(Name = "Invoice To Date")]
        public string InvoiceToDate { get; set; }
        [Display(Name = "Ivonices")]
        public List<InvoiceViewModel> Invoices { get; set; }

        public static implicit operator InvoiceSearchDataModel(InvoiceRequestViewModel source)
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (DateTime.TryParse(source.InvoiceFromDate, out DateTime dt))
            {
                fromDate = dt;
            }
            if (DateTime.TryParse(source.InvoiceToDate, out dt))
            {
                toDate = dt;
            }

            return new InvoiceSearchDataModel
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                InvoiceId = source.InvoiceId,
                InvoiceFromDate = fromDate,
                InvoiceToDate = toDate
            };
        }
    }
}
