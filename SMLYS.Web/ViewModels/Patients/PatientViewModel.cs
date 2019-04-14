using SMLYS.ApplicationCore.DTOs.Patients;
using SMLYS.Web.ViewModels.Adresses;
using System.ComponentModel.DataAnnotations;

namespace SMLYS.Web.ViewModels.Patients
{
    public class PatientViewModel
    {
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }
        [Display(Name = "Family Id")]
        public int FamilyId { get; set; }
        [Display(Name = "Doctor Id")]
        public int DoctorId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Primary Member")]
        public bool PrimaryMember { get; set; }
        [Required]
        [Display(Name = "Minor")]
        public bool Minor { get; set; }

        public AddressViewModel Address { get; set; }

        public virtual FamilyViewModel Family { get; set; }

        public static implicit operator PatientViewModel(PatientModel source)
        {
            return new PatientViewModel
            {
                DoctorId = source.DoctorId,
                FamilyId = source.FamilyId,
                PatientId = source.PatientId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                
                Address = new AddressViewModel
                {
                    Address1 = source.Address.Address1,
                    Address2 = source.Address.Address2,
                    AttentionTo = source.Address.AttentionTo,
                    City = source.Address.City,
                    CountryId = source.Address.CountryId,
                    PostalCode = source.Address.PostalCode,
                    RegionId = source.Address.RegionId,
                    RegionName = source.Address.RegionName,
                    CountryName = source.Address.CountryName
                }
            };
        }
    }
}
