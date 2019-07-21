using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SMLYS.Web.Models
{
    [DataContract(Name = "web_search_request_detail")]
    public class WebSearchRequestDetailModel
    {
        [DataMember(Name = "search_type")]
        public string SearchType { get; set; }
        [DataMember(Name = "search_content")]
        public string SearchContent { get; set; }
    }
}
