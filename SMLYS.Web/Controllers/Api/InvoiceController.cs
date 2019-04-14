using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.Interfaces.Services.Items;
using SMLYS.ApplicationCore.Interfaces.Services.Taxes;
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.Models.Patients;
using SMLYS.Web.ViewModels.Patients;

namespace SMLYS.Web.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IPatientApiService _patientApiService;
        private readonly IItemService _itemService;
        private readonly ITaxService _taxService;
        private readonly UserHandler _userHandler;

        public InvoiceController(IPatientApiService patientApiService, IItemService itemService, 
            ITaxService taxService, UserHandler userHandler)
        {
            _patientApiService = patientApiService;
            _itemService = itemService;
            _taxService = taxService;
            _userHandler = userHandler;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<controller>/5
        [Route("[action]/{familyId}")]
        public IActionResult GetInitData(int familyId)
        {
            var userContext = _userHandler.GetUserContext();
            var taxes = _taxService.SearchTaxesAsync(userContext.ClinicCountryId, userContext.ClinicRegionId, false);
            var items = _itemService.SearchItems();

            var data = new { Items = items, Taxes = taxes };
            return Json(data);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]List<PatientRequestModel> patients)
        {
            var result = _patientApiService.CreateNewPatient(patients);
            return Json(result);
        }

        // POST api/<controller>/PostSearchPatients
        [Route("[action]")]
        [HttpPost]
        public IActionResult PostSearchPatients([FromBody]List<SearchPatientRequestModel> patients)
        {
            var result = _patientApiService.SearchPatients(patients);
            return Json(result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
