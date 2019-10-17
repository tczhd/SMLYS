
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SMLYS.Web.Models;
using Microsoft.AspNetCore.Http;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.Interfaces.Services.ThirdParty.PaymentGateway.Common;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Helcim;
using SMLYS.ApplicationCore.DTOs.ThirdPartyService.PaymentGateway.Common;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft;
using SMLYS.ApplicationCore.Enums;

namespace SMLYS.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private JobsSearch _jobsSearch = new JobsSearch();
        //private static SearchServiceClient _searchClient;
        //private static ISearchIndexClient _indexClient;
        //// private static string IndexName = "nycjobs";
        //private static string IndexName = "azuresql-index";
        public static string errorMessage;

        private readonly IThirdPartyPaymentService _helcimPaymentService;
        public HomeController(UserHandler userHandler, IThirdPartyPaymentService helcimPaymentService)
            : base(userHandler)
        {
            _helcimPaymentService = helcimPaymentService;
        }

        private void InitSearch()
        {
            //string searchServiceName = ConfigurationManager.AppSettings["SearchServiceName"];
            //string apiKey = ConfigurationManager.AppSettings["SearchServiceApiKey"];

            //string searchServiceName = "smyls-patient";
            //string apiKey = "7EA6F8B6137FAA3A9D3BCFEA7833D720";

            //// Create a reference to the NYCJobs index
            //_searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
            //_indexClient = _searchClient.Indexes.GetClient(IndexName);
        }

        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = view;

            var actionResult = GetActionResult();
            if (actionResult != null)
            {
                return actionResult;
            }
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

        public ActionResult Suggest(string searchType, bool highlights, bool fuzzy, string term)
        {
            //InitSearch();

            //// Call suggest API and return results
            //SuggestParameters sp = new SuggestParameters()
            //{
            //    UseFuzzyMatching = fuzzy,
            //    Top = 5
            //};

            //if (highlights)
            //{
            //    sp.HighlightPreTag = "<b>";
            //    sp.HighlightPostTag = "</b>";
            //}

            //var suggestResult = _indexClient.Documents.Suggest(term, "sg", sp);
            var suggestResult = _jobsSearch.Suggest(GetIndexNameType(searchType), highlights, fuzzy, term);
            // Convert the suggest query results to a list that can be displayed in the client.
            //var suggestions = suggestResult.Results.Select(x => x.Text).ToList();
            var suggestions = suggestResult.Results.Select(x => new { label = $"{x.Document["FirstName"]},{x.Document["LastName"]},{x.Document["Phone"]}"
                , value = x.Document["Id"]
            }).ToList();
            //var data = suggestions.Select(p => new { label = p, value = "1" });
            return new JsonResult(suggestions);
            //return new JsonResult(new
            //{
            //    JsonRequestBehavior = 0,
            //    Data = suggestions
            //});
        }

        public ActionResult Search(string searchType, string q = "", int currentPage = 0)
        {
            string businessTitleFacet = "";
            string postingTypeFacet = "";
            string salaryRangeFacet = "";
            string sortType = "";
            double lat = 40.736224;
            double lon = -73.99251;
            int zipCode = 10001;
            int maxDistance = 0;
            // If blank search, assume they want to search everything
            if (string.IsNullOrWhiteSpace(q))
                q = "*";

            string maxDistanceLat = string.Empty;
            string maxDistanceLon = string.Empty;

            var response = _jobsSearch.Search(GetIndexNameType(searchType), q, businessTitleFacet, postingTypeFacet, salaryRangeFacet, sortType, lat, lon, currentPage, maxDistance, maxDistanceLat, maxDistanceLon);
            return new JsonResult
            (
                new { results = response.Results, facets = response.Facets, count = (int)response.Count }
           );
        }

        public ActionResult AutoComplete(string searchType, string term)
        {
            //InitSearch();
            ////Call autocomplete API and return results
            //AutocompleteParameters ap = new AutocompleteParameters()
            //{
            //    AutocompleteMode = AutocompleteMode.OneTermWithContext,
            //    UseFuzzyMatching = false,
            //    Top = 5
            //};
            //AutocompleteResult autocompleteResult = _indexClient.Documents.Autocomplete(term, "sg", ap);
            AutocompleteResult autocompleteResult = _jobsSearch.AutoComplete(GetIndexNameType(searchType), term);
            // Conver the Suggest results to a list that can be displayed in the client.
            List<string> autocomplete = autocompleteResult.Results.Select(x => x.Text).ToList();
            return new JsonResult(autocomplete);
            //return new JsonResult(new
            //{
            //    JsonRequestBehavior = 0,
            //    Data = autocomplete
            //});
        }

        private IndexNameType GetIndexNameType(string searchType)
        {
            int typeId = int.Parse(searchType);
            return (IndexNameType)typeId;
        }
        //public ActionResult Facets()
        //{
        //    InitSearch();

        //    // Call suggest API and return results
        //    SearchParameters sp = new SearchParameters()
        //    {
        //        Facets = new List<string> { "agency,count:500" },
        //        Top = 0
        //    };


        //    var searchResult = _indexClient.Documents.Search("*", sp);

        //    // Convert the suggest query results to a list that can be displayed in the client.

        //    List<string> facets = searchResult.Facets["agency"].Select(x => x.Value.ToString()).ToList();
        //    return new JsonResult(facets);
        //    //return new JsonResult(new
        //    //{
        //    //    JsonRequestBehavior = 0,
        //    //    Data = facets
        //    //});
        //}


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
