using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Patients
{
    public class FamilyViewModel
    {
        [Display(Name = "family_id")]
        public int FamilyId { get; set; }
        [Display(Name = "family_name")]
        public string FamilyName{ get; set; }
    }
}
