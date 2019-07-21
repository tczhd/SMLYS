using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models
{
    [DataContract(Name = "web_search_request")]
    public class WebSearchRequestModel
    {
        [DataMember(Name = "current_page")]
        public int CurrentPage { get; set; }
        [DataMember(Name = "web_search_request_detail")]
        public List<WebSearchRequestDetailModel> WebSearchRequestDetail { get; set; }
    }
}
