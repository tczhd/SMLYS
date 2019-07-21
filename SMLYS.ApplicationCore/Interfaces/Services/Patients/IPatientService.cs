using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Patients;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLYS.ApplicationCore.Interfaces.Services.Patients
{
    public interface IPatientService
    {
        List<Patient> CreatePatientAsync(List<Patient> patient);

        List<Patient> SearchPatientAsync(List<GenericSearchParameter> searchParameters, int currentPage, int pageSize);
        int SearchPatientCountAsync(List<GenericSearchParameter> searchParameters);
        Patient SearchPatientAsync(int id);

        PatientModel SearchPatientModelAsync(int id);

        List<PatientModel> SearchPatientsAsync(int familyId);

    }
}
