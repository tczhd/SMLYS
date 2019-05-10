
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

        public HomeController(UserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = view;

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
