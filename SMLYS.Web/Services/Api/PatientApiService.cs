
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Interfaces.Services.Patients;
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMLYS.ApplicationCore.Entities.CommonAggregate;
using System.Linq;
using SMLYS.ApplicationCore.DTOs.Patients;

namespace SMLYS.Web.Services.Api
{
    public class PatientApiService : IPatientApiService
    {
        public readonly IPatientService _patientService;

        public PatientApiService(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public  PatientResultViewModel CreateNewPatient(List<PatientRequestModel> newPatients)
        {          
            var patients = new List<Patient>();

            var family = new Family
            {
                Name = newPatients[0].LastName
            };

            foreach (PatientRequestModel newPatient in newPatients)
            {
                var patient = new Patient();

                patient.Address = new Address()
                {
                    CreatedDateUtc = DateTime.UtcNow,
                    AddressTypeId = 1,
                    Address1 = newPatient.Address1,
                    Address2 = newPatient.Address2,
                    AttentionTo = "",
                    City = newPatient.City,
                    CountryId = newPatient.CountryId,
                    RegionId = newPatient.StateId,
                    PostalCode = newPatient.PostalCode,
                    CreatedBy = 1
                };

                patient.Id = newPatient.PatientId;
                patient.FirstName = newPatient.FirstName;
                patient.LastName = newPatient.LastName;
                patient.ClinicId = 1;
                patient.Age = 30;
                patient.Status = 1;
                patient.Title = "Mr.";
                patient.Gender = 1;
                patient.Phone = newPatient.Phone;
                patient.Email = newPatient.Email;
                patient.CreatedDateUtc = DateTime.UtcNow;
                patient.CreatedBy = 1;
                patient.Family = family;
                patient.PrimaryMember = true;
                patient.Minor = false;
                patient.DoctorId = 1;

                patients.Add(patient);
            }

           _patientService.CreatePatientAsync(patients);

            var result = new PatientResultViewModel {  Success = true, Message= "Add patient success. ", PatienId = patients.First().Id};

            return  result;
        }

        public PatientViewModel SearchPatient(int id)
        {
            var patient = _patientService.SearchPatientAsync(id);
            if (patient != null)
            {
                var data = new PatientViewModel() {
                    
                    PatientId = patient.Id,
                    Phone = patient.Phone,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    Family = new FamilyViewModel {
                        FamilyId = patient.FamilyId,
                        FamilyName = patient.Family.Name
                    },
                    Address = new ViewModels.Adresses.AddressViewModel {
                        Address1 = patient.Address.Address1,
                        Address2 = patient.Address.Address2,
                        City = patient.Address.City,
                        CountryId = patient.Address.CountryId,
                        RegionId  = patient.Address.RegionId,
                        PostalCode = patient.Address.PostalCode
                    }

                };
                return data;
            }

            return null;
        }

        public SearchPatientResultViewModel SearchPatients(List<SearchPatientRequestModel> searchPatientRequestModels)
        {
            var searchPatientFilter = searchPatientRequestModels.Select(p => new SearchPatientParameter {
                SearchType = p.SearchType,
                SearchContent = p.SearchContent
            }).ToList();
            var data = _patientService.SearchPatientAsync(searchPatientFilter);

            SearchPatientResultViewModel result = new SearchPatientResultViewModel() {
                Success = true,
                Message = "Search patients success. "
            };

            result.PatientDetail = data.Select(p => new SearchPatientDetailResultViewModel {
                PatientAddress = $"{p.Address.Address1}, {p.Address.City} {p.Address.RegionNavigation.Name}" ,
                PatientEmail = p.Email,
                PatientName = $"{p.FirstName } {p.LastName}",
                PatientPhone = p.Phone,
                PatientStatus = p.CreatedDateUtc.ToString("MMM dd"),
                PatientId = p.Id

            }).ToList();

            return result;
        }
    }
}
