
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.Web.Interfaces.Api;

namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientApiService _patientApiService;

        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = $"Patient - {view}";

            return View(view);
        }

    }
}
