using SMLYS.Web.ViewModels.Adresses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMLYS.Web.ViewModels.SiteUsers
{
    public class SiteUserViewModel
    {
        [Display(Name = "patient_form_type")]
        public string PatientFormType { get; set; }
        [Display(Name = "site_user_id")]
        public int SiteUserId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Site User Level Name")]
        public string SiteUserLevelName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
