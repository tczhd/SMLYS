using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models.Patients
{
    [DataContract(Name = "patient_group")]
    public class PatientGroupRequestModel
    {
        [DataMember(Name = "patients")]
        public List<PatientRequestModel> Patients { get; set; }
    }
}
