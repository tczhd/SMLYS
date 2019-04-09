using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Patients
{
    [DataContract(Name ="patient")]
    public class PatientRequestModel
    {
        [DataMember(Name = "patient_id")]
        public int PatientId { get; set; }
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "company")]
        public string Company { get; set; }
        [DataMember(Name = "address1")]
        public string Address1 { get; set; }
        [DataMember(Name = "address2")]
        public string Address2 { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }
        [DataMember(Name = "state_id")]
        public int StateId { get; set; }
        [DataMember(Name = "country_id")]
        public int CountryId { get; set; }
        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }
        [DataMember(Name = "phone")]
        public string Phone { get; set; }
    }
}


