using JW;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMLYS.Web.ViewModels.Base
{
    public class BasicRequestViewModel : PageModel
    {
        [Display(Name = "Success")]
        public bool Success { get; set; }
        [Display(Name = "Error Message")]
        public string ErrorMessage { get; set; }
        public Pager Pager { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int MaxPages { get; set; }

        public BasicRequestViewModel() {
            CurrentPage = 1;
            MaxPages = 5;
            PageSize = 2;
            Pager = new Pager(0, CurrentPage, PageSize, MaxPages);
        }
    }
}
