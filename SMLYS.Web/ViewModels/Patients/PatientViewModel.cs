using SMLYS.Web.ViewModels.Adresses;
using System.ComponentModel.DataAnnotations;

namespace SMLYS.Web.ViewModels.Patients
{
    public class PatientViewModel
    {
        [Display(Name = "patient_form_type")]
        public string PatientFormType { get; set; }
        [Display(Name = "patient_id")]
        public int PatientId { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
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
