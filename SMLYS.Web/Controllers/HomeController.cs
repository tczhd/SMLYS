
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SMLYS.Web.Models;
using Microsoft.AspNetCore.Http;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.User;
using SMLYS.ApplicationCore.Interfaces.Base;
using SMLYS.RazorClassLib.Services;
using SMLYS.RazorClassLib.Views.Emails.ConfirmAccount;
using System.Threading.Tasks;

namespace SMLYS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserHandler _userHandler;
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public HomeController(UserHandler userHandler, IEmailSender emailSender,
            IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _userHandler = userHandler;
            _emailSender = emailSender;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        [Route("{view=Index}")]
        public async Task<IActionResult> Index(string view)
        {
            ViewData["Title"] = view;

            //var invoiceModel = new SMLYS.Web.Views.Emails.Invoices.InvoiceModel();
            //string body = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/Invoices/Invoice.cshtml", invoiceModel);

            //await _emailSender.SendEmailAsync("hongdingzhu@gmail.com", "Test", "Test content", body);

            return View(view);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
