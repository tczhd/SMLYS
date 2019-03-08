
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMLYS.Web.Services.Api
{
    public class PatientApiService : IPatientApiService
    {
        public Task<PatientResultViewModel> CreateNewPatient(List<PatientRequestModel> Patients)
        {
            throw new NotImplementedException();
        }
    }
}
