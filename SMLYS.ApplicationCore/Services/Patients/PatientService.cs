using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Patients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLYS.ApplicationCore.Services.Patients
{
    public class PatientService : IPatientService
    {
        private readonly IAsyncRepository<Patient> _patientRepository;

        public PatientService(IAsyncRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            patient.Address = new Entities.CommonAggregate.Address();

            throw new NotImplementedException();
        }
    }
}
