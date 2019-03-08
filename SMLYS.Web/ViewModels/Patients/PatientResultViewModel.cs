
using System.Runtime.Serialization;


namespace SMLYS.Web.ViewModels.Patients
{
    [DataContract(Name = "patient_result")]
    public class PatientResultViewModel
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
