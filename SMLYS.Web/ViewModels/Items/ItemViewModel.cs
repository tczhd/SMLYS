using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Items
{
    public class ItemViewModel
    {
        [Display(Name = "Item Number")]
        public int? ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }
       
    }
}
