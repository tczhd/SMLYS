
using SMLYS.Web.ViewModels.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace SMLYS.Web.ViewModels.Patients
{
    [DataContract(Name = "search_patient_result")]
    public class SearchPatientResultViewModel : BaseResultViewModel
    {
        [DataMember(Name = "patient_detail")]
        public List<SearchPatientDetailResultViewModel> PatientDetail { get; set; }
    }
}
