
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

        public PatientController(IPatientApiService patientApiService)
        {
            _patientApiService = patientApiService;
        }

        [Route("{view=Index}")]
        public IActionResult Index(int id, string view)
        {
            if (view == "PatientForm")
            {
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
