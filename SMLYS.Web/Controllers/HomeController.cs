
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SMLYS.Web.Models;
using Microsoft.AspNetCore.Http;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.User;

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

        public IActionResult Index()
        {
            //var data = _userHandler.GetUserContext();
  
            return View();
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
