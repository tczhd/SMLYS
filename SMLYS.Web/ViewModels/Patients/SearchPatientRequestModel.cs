using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Patients
{
    [DataContract(Name = "search_patient")]
    public class SearchPatientRequestModel
    {
        [DataMember(Name = "search_type")]
        public string SearchType { get; set; }
        [DataMember(Name = "search_content")]
        public string SearchContent { get; set; }
    }
}
