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
                    _patientRepository.AddOnly(patient);
                }

               _patientRepository.SaveAll();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

            return patients;
        }

        public List<Patient> SearchPatientAsync(List<SearchPatientParameter> searchPatientParameter)
        {
            var patientSpecification = new PatientSpecification();

            foreach (var parameter in searchPatientParameter)
            {
                if (parameter.SearchType == "first_name")
                {
                    patientSpecification.AddFirstName(parameter.SearchContent);
                }
                else if (parameter.SearchType == "last_name")
                {
                    patientSpecification.AddLastName(parameter.SearchContent);
                }
            }

            return  _patientRepository.List(patientSpecification).ToList();
        }
    }
}
