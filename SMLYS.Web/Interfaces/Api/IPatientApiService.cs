using SMLYS.Web.Models;
using SMLYS.Web.Models.Patients;
using SMLYS.Web.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.Interfaces.Api
{
    public interface IPatientApiService
    {
        PatientResultViewModel CreateNewPatient(List<PatientRequestModel> Patients);
        SearchPatientResultViewModel SearchPatients(WebSearchRequestModel searchequestModel);
        PatientViewModel SearchPatient(int id);
    }
}
