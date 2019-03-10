using System.Runtime.Serialization;

namespace SMLYS.Web.ViewModels.Base
{
    [DataContract(Name = "base_result")]
    public class BaseResultViewModel
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
