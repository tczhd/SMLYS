
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.Interfaces.Services.Utiliites;
using SMLYS.Web.Interfaces.Api;

namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("Patient")]
    public class PatientController : Controller
    {
        private readonly IUtilityService _utilityService;
       private readonly IPatientApiService _patientApiService;

        public PatientController(IPatientApiService patientApiService, IUtilityService utilityService)
        {
            _patientApiService = patientApiService;
            _utilityService = utilityService;
        }

        [Route("{view=Index}")]
        public IActionResult Index(int id, string view)
        {
            if (view == "PatientForm")
            {
                ViewBag.ListofCountry = _utilityService.GetCountries();

                if (id == 0)
                {
                    ViewData["Title"] = $"New Patient";
                    ViewData["FormType"] = $"New Patient";
                }
                else
                {
                    ViewData["Title"] = $"Edit Patient";
                    ViewData["FormType"] = $"Edit Patient";

                    var data = _patientApiService.SearchPatient(id);
                    return View(view, data);
                }
            }
            else if (view == "Index")
            {
                ViewData["Title"] = $"Search Patient";
            }

            return View(view);
        }

    }
}
