using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("Invoice")]
    public class InvoiceController : Controller
    {
        [Route("{view=Index}")]
        public IActionResult Index(int id, string view)
        {
            if (view == "InvoiceForm")
            {
                if (id == 0)
                {
                    ViewData["Title"] = $"New Invoice";
                    ViewData["FormType"] = $"New Invoice";
                }
                else
                {
                    ViewData["Title"] = $"Edit Invoice";
                    ViewData["FormType"] = $"Edit Invoice";

                    return View(view);
                }
            }
            else if (view == "Index")
            {
                ViewData["Title"] = $"Search Invoice";
            }

            return View(view);
        }
    }
}
