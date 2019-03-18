
using SMLYS.Web.ViewModels.Base;
using System.Runtime.Serialization;


namespace SMLYS.Web.ViewModels.Patients
{
    [DataContract(Name = "search_patient_detail_result")]
    public class SearchPatientDetailResultViewModel 
    {
        [DataMember(Name = "patient_id")]
        public int PatientId { get; set; }
        [DataMember(Name = "patient_name")]
        public string PatientName { get; set; }
        [DataMember(Name = "patient_address")]
        public string PatientAddress { get; set; }
        [DataMember(Name = "patient_phone")]
        public string PatientPhone { get; set; }
        [DataMember(Name = "patient_email")]
        public string PatientEmail { get; set; }
        [DataMember(Name = "patient_status")]
        public string PatientStatus { get; set; }
    }
}
