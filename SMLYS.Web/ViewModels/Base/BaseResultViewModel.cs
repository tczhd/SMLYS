using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace SMLYS.Web.ViewModels.Base
{
    [DataContract(Name = "base_result")]
    public class BaseResultViewModel
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "current_page")]
        public int CurrentPage { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "total_pages")]
        public int TotalPages
        {
            get
            {
                return Count / WebSiteSettings.PageSize + ((Count % WebSiteSettings.PageSize > 0) ? 1 : 0);
            }
        }

        [DataMember(Name = "pages")]
        public List<int> Pages
        {
            get
            {
                var Pages = new List<int>();
                var minPages = Math.Min(TotalPages, WebSiteSettings.MaxPages);
                for (int i = 1; i <= minPages; i++)
                {
                    Pages.Add(i);
                }
                return Pages;
            }
        }
    }
}
