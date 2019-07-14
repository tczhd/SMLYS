using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.Interfaces.Services.Doctor;
using SMLYS.ApplicationCore.Interfaces.Services.Invoices;
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
        private readonly IDoctorService _doctorService;
        private readonly IInvoiceService _invoiceService;
        private readonly UserHandler _userHandler;

        public InvoiceController(IPatientService patientService, IDoctorService doctorService, 
            IUtilityService utilityService, IInvoiceService invoiceService, UserHandler userHandler)
        {
            _patientService = patientService;
            _utilityService = utilityService;
            _doctorService = doctorService;
            _invoiceService = invoiceService;
            _userHandler = userHandler;
        }

        [Route("{view=Index}")]
        public IActionResult Index(int id, string view, int patientId, int invoiceId)
        {
            var userContext = _userHandler.GetUserContext();
            if (view == "InvoiceForm")
            {
                if (id == 0)
                {
                    ViewData["Title"] = $"New Invoice";
                    ViewData["FormType"] = $"New Invoice";

                    var patient = _patientService.SearchPatientModelAsync(patientId);
                    if (patient != null)
                    {
                        var patients = _patientService.SearchPatientsAsync(patient.FamilyId);

                        ViewBag.ListofPatient = patients.Select(p => new ListItemModel
                        {
                            Id = p.PatientId,
                            Name = $"{p.FirstName} {p.LastName}"
                        }).ToList();

                        var doctors = _doctorService.SearchDoctorsAsync(userContext.ClinicId);

                        ViewBag.ListofDoctor = doctors.Select(p => new ListItemModel
                        {
                            Id = p.DoctorId,
                            Name = $"{p.FirstName} {p.LastName}"
                        }).ToList();

                        var data = new InvoiceViewModel
                        {
                            FamilyId = patient.FamilyId,
                            PatientId = patient.PatientId,
                            DoctorId = patient.DoctorId,
                            InvoiceDate = DateTime.Now,
                            PatientName = $"{patient.FirstName} {patient.LastName}",
                            Patients = patients.Select(p => (PatientViewModel)p).ToList()
                        };
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
            else if (view == "InvoiceDetail")
            {
                ViewData["Title"] = $"Invoice Detail";

                var data = _invoiceService.SearchInvoice(invoiceId);
                if (data != null)
                {
                    var viewData = (InvoiceViewModel)data;

                    return View(view, viewData);
                }
                else
                {
                    var result = new BaseResultViewModel();
                    result.Message = "Invoice not found. ";

                    return View("_SharedResult", result);
                }
            }
            else if (view == "Index")
            {
                ViewData["Title"] = $"Search Invoice";

               // var data = _invoiceService.SearchInvoices();
                //var viewData = data.Select(p => (InvoiceViewModel)p).ToList();

                var viewData = new InvoiceRequestViewModel();
                return View(view, viewData);
            }

            return View(view);
        }


        [HttpPost]
        public IActionResult SearchInvoice(InvoiceRequestViewModel invoiceRequestViewModel)
        {

            if (ModelState.IsValid)
            {
                var invoice = _invoiceService.SearchInvoices(invoiceRequestViewModel);
                invoiceRequestViewModel.Invoices = invoice.Select(p => (InvoiceViewModel)p).ToList();
            }
            else
            {
                invoiceRequestViewModel.Invoices = new List<InvoiceViewModel>();
            }

            return View("Index", invoiceRequestViewModel);

        }
    }
}
