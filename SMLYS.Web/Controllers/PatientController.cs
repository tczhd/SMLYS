using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("Patient")]
    public class PatientController : Controller
    {
        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = $"Patient - {view}";

            return View(view);
        }
    }
}
