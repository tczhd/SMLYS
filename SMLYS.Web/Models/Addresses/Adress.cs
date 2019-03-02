using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Adresses
{
    public class AddressViewModel
    {

        [Display(Name = "Address Type")]
        public string AddressType { get; set; }

        [Display(Name = "Address1")]
        public string Address1 { get; set; }
    
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public int? RegionId { get; set; }

        [Display(Name = "State Name")]
        public string Region { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Attention To")]
        public string AttentionTo { get; set; }
    }
}
