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
    [Route("Clinic")]
    public class ClinicController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(string view)
        {
            ViewData["Title"] = view;

            //test();

            return View(view);
        }
    }
}
