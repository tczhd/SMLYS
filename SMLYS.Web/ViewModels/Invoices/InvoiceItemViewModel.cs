using SMLYS.ApplicationCore.DTOs.Invoices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Invoices
{
    public class InvoiceItemViewModel
    {
        [Display(Name = "Item Id")]
        public int ItemId { get; set; }
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }
        [Display(Name = "Tax")]
        public decimal Tax { get; set; }
        [Display(Name = "SubTotal")]
        public decimal SubTotal { get; set; }

        public static implicit operator InvoiceItemViewModel(InvoiceItemModel source)
        {
            return new InvoiceItemViewModel
            {
               ItemId = source.ItemId,
               ItemName = source.ItemName,
               Quantity = source.Quantity,
               Cost = source.Price,
               Tax = source.TaxTotal,
               SubTotal = source.Subtotal
            };
        }
    }
}
