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
        [Display(Name = "ServiceGroupId")]
        public int? ServiceGroupId { get; set; }
        [Display(Name = "ShortCode")]
        public string ShortCode { get; set; }
        [Display(Name = "IndustryCodeId")]
        public int? IndustryCodeId { get; set; }
        [Display(Name = "Subscription")]
        public bool Subscription { get; set; }
        [Display(Name = "Subscription")]
        public string SubscriptionDisplay { get; set; }

    }
}
