using SMLYS.ApplicationCore.DTOs.Patients;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Patients;
using SMLYS.ApplicationCore.Specifications.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLYS.ApplicationCore.Services.Patients
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientService(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public List<Patient> CreatePatientAsync(List<Patient> patients)
        {
            try
            {
                foreach (Patient patient in patients)
                {
                    _patientRepository.Add(patient);
                }

               // await _patientRepository.SaveAllAsync();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

            return patients;
        }

        public List<Patient> SearchPatientAsync(List<SearchPatientParameter> searchPatientParameter)
        {
            var patientSpecification = new PatientSpecification(searchPatientParameter[0].SearchContent);

            foreach (var parameter in searchPatientParameter)
            {
                if (parameter.SearchType == "first_name")
                {
                    patientSpecification.AddFirstName(parameter.SearchContent);
                }
            }

            return  _patientRepository.List(patientSpecification).ToList();
        }
    }
}
