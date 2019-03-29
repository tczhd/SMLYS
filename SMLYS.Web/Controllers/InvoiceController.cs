using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.Interfaces.Services.Patients;
using SMLYS.ApplicationCore.Interfaces.Services.Utiliites;
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.ViewModels.Base;
using SMLYS.Web.ViewModels.Invoices;
using SMLYS.Web.ViewModels.Patients;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("Invoice")]
    public class InvoiceController : Controller
    {

        private readonly IUtilityService _utilityService;
        private readonly IPatientService _patientService;

        public InvoiceController(IPatientService patientService, IUtilityService utilityService)
        {
            _patientService = patientService;
            _utilityService = utilityService;
        }

        [Route("{view=Index}")]
        public IActionResult Index(int id, string view, int patientId)
        {
            if (view == "InvoiceForm")
            {
                if (id == 0)
                {
                    ViewData["Title"] = $"New Invoice";
                    ViewData["FormType"] = $"New Invoice";

                    var patient = _patientService.SearchPatientAsync(patientId);
                    if (patient != null)
                    {
                        var data = new InvoiceViewModel { PatientId = patient.Id, PatientName = $"{patient.FirstName} {patient.LastName}" };
                        return View(view, data);
                    }
                    else
                    {
                        var result = new BaseResultViewModel();
                        result.Message = "Patient not found. ";

                        return View("_SharedResult", result);
                    }

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
