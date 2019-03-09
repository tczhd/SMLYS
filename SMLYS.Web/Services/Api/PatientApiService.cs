
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Interfaces.Services.Patients;
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMLYS.ApplicationCore.Entities.CommonAggregate;

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
                    CountryId = 38,
                    RegionId = 54,
                    CreatedBy = 1
                };

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

            var result = new PatientResultViewModel {  Success = true};

            return  result;
        }
    }
}
