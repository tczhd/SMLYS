
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SMLYS.Web.Models;
using Microsoft.AspNetCore.Http;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.Interfaces.Services.ThirdParty.PaymentGateway.Common;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Helcim;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;

namespace SMLYS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserHandler _userHandler;
        private readonly IThirdPartyPaymentService _helcimPaymentService;
        public HomeController(UserHandler userHandler, IThirdPartyPaymentService helcimPaymentService)
        {
            _userHandler = userHandler;
            _helcimPaymentService = helcimPaymentService;
        }

        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = view;

            //test();

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

        private void test()
        {
            ////var request = new HelcimBasicRequestModel()
            ////{,
            ////    TransactionType = "purchase",
            ////    TerminalId = "70028",
            ////    Test = true,
            ////    Amount = 40,
            ////    CardToken = "defcbf159f5434f7bbd6a3",
            ////    CardF4L4 = "54545454",
            ////    Comments = "Card on file payment"
            ////    //CreditCard = new HelcimPaymentRequestModel()
            ////    //{
            ////    //    CardHolderName = "Jane Smith",
            ////    //    cardNumber = "5454545454545454",
            ////    //    cardExpiry = "1020",
            ////    //    cardCVV = "100",
            ////    //    cardHolderAddress = "123 Road Street",
            ////    //    cardHolderPostalCode = "90212"
            ////    //}
            ////};

            ////var result = _helcimPaymentService.ProcessPayment(request);

            var request = new HelcimBasicRequestModel()
            {
                AccountId = "2500318950",
                ApiToken = "NXK54k3T92M433HK2ec6fFgJS",
                TransactionId = "3995866",
                Amount = 20,
                CreditCard = new HelcimCreditCardRequestModel()
                {
                    CardHolderName = "Jane Smith",
                    cardNumber = "5454545454545454",
                    cardExpiry = "1020",
                    cardCVV = "100",
                }
            };

            var result = _helcimPaymentService.ProcessRefund(request);
        }
    }
}
