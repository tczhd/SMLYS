using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.ViewModels.Patients;

namespace SMLYS.Web.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IPatientApiService _patientApiService;

        public InvoiceController(IPatientApiService patientApiService)
        {
            _patientApiService = patientApiService;
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
        public string GetInitData(int familyId)
        {
            return "value";
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
