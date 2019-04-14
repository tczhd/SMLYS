
using SMLYS.Web.ViewModels.Base;
using System.Runtime.Serialization;


namespace SMLYS.Web.Models.Patients
{
    [DataContract(Name = "create_patient_result")]
    public class PatientResultViewModel : BaseResultViewModel
    {
        [DataMember(Name = "patient_id")]
        public int PatienId { get; set; }
    }
}
