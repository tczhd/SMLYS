using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Invoices;
using SMLYS.ApplicationCore.Interfaces.Base;
using SMLYS.ApplicationCore.Interfaces.Services.Invoices;
using SMLYS.ApplicationCore.Interfaces.Services.Items;
using SMLYS.ApplicationCore.Interfaces.Services.Taxes;
using SMLYS.RazorClassLib.Services;
using SMLYS.Web.Interfaces.Api;
using SMLYS.Web.Models;
using SMLYS.Web.Models.Invoices;
using SMLYS.Web.Models.Patients;
using SMLYS.Web.ViewModels.Patients;

namespace SMLYS.Web.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IItemService _itemService;
        private readonly ITaxService _taxService;
        private readonly UserHandler _userHandler;
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public InvoiceController(IInvoiceService invoiceService, IItemService itemService, 
            ITaxService taxService, UserHandler userHandler, IEmailSender emailSender, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _invoiceService = invoiceService;
            _itemService = itemService;
            _taxService = taxService;
            _userHandler = userHandler;
            _emailSender = emailSender;
            _razorViewToStringRenderer = razorViewToStringRenderer;
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
        public IActionResult Post([FromBody]InvoiceRequestModel invoice)
        {
            var userContext = _userHandler.GetUserContext();
            var taxes = _taxService.SearchTaxesAsync(userContext.ClinicCountryId, userContext.ClinicRegionId, false);
            var taxRate = taxes.Sum(p => p.TaxRate);
            var invoiceItemModels = invoice.InvoiceItems.Select(p => new InvoiceItemModel {
                ItemId = p.ItemId,
                Price = p.Cost,
                Quantity = p.Quantity,
                TaxTotal = p.Cost * p.Quantity * taxRate,
                Subtotal = p.Cost * p.Quantity,
                Total = p.Cost * p.Quantity * (1 + taxRate)
            }).ToList();


            var invoiceModel = new InvoiceModel {
                AmountPaid = 0,
                Note = "New Invoice",
                InvoiceStatus = 1,
                InvoiceItems = invoiceItemModels,
                PatientId = invoice.PatientId,
                PaymentStatus = 1,
                InvoiceDate = DateTime.Now,
                DoctorId = invoice.DoctorId,
                ClinicId = userContext.ClinicId,
                CreatedBy = userContext.SiteUserId,
                DiscountTotal = 0,
                ReOccouring = false,
                Subtotal = invoiceItemModels.Sum(p => p.Subtotal),
                TaxTotal = invoiceItemModels.Sum(p => p.TaxTotal),
                Total = invoiceItemModels.Sum(p => p.Total),
                UpdatedDateUTC = DateTime.UtcNow
            };

            var result = _invoiceService.CreateInvoiceAsync(invoiceModel);
            return Json(result);
        }

        // POST api/<controller>/PostSendInvoiceEmail
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> PostSendInvoiceEmail([FromBody]InvoiceEmailRequestModel invoice)
        {
            var result = new ResultModel();

            var invoiceModel = _invoiceService.SearchInvoice(invoice.InvoiceId);
            if (invoiceModel != null)
            {

                try
                {
                    string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/Invoices/Invoice.cshtml", invoiceModel);

                    await _emailSender.SendEmailAsync(invoiceModel.PatientEmail, "SMLYS invoice", string.Empty, body);

                    result.Success = true;
                    result.Message = "Email has been sent successfully.";
                }
                catch (Exception ex)
                {
                    result.Message = "Oops, Email was not sent. please try again. ";
                }
            }
            else {
                result.Message = "Invalid invoice Id, Please choose right one and try again. ";
            }

            return Json(result);
        }

        // POST api/<controller>/PostApplyPayment
        [Route("[action]")]
        [HttpPost]
        public IActionResult PostApplyPayment([FromBody]InvoicePaymentRequestModel invoicePayment)
        {
            var result = new ResultModel();

            //var invoiceModel = _invoiceService.SearchInvoice(invoice.InvoiceId);
            //if (invoiceModel != null)
            //{

            //    try
            //    {
            //        string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/Invoices/Invoice.cshtml", invoiceModel);

            //        await _emailSender.SendEmailAsync(invoiceModel.PatientEmail, "SMLYS invoice", string.Empty, body);

            //        result.Success = true;
            //        result.Message = "Email has been sent successfully.";
            //    }
            //    catch (Exception ex)
            //    {
            //        result.Message = "Oops, Email was not sent. please try again. ";
            //    }
            //}
            //else
            //{
            //    result.Message = "Invalid invoice Id, Please choose right one and try again. ";
            //}

            return Json(result);
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
