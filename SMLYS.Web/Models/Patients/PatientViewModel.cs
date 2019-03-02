﻿using SMLYS.Web.Models.Adresses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Patients
{
    public class PatientViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public int Gender { get; set; }
        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Note")]
        public string Note { get; set; }
        [Required]
        [Display(Name = "Primary Member")]
        public bool PrimaryMember { get; set; }
        [Required]
        [Display(Name = "Minor")]
        public bool Minor { get; set; }

        public AddressViewModel Address { get; set; }
        //public virtual Clinic Clinic { get; set; }
        //public virtual SiteUser CreatedByNavigation { get; set; }
        //public virtual Doctor Doctor { get; set; }
        //public virtual Family Family { get; set; }
        //public virtual PatientStatus StatusNavigation { get; set; }
        //public virtual SiteUser UpdatedByNavigation { get; set; }
    }
}
