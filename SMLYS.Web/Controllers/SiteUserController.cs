using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("SiteUser")]
    public class SiteUserController : Controller
    {
       
        [Route("{view=Index}")]
        public IActionResult Index(int id, string view)
        {
            if (view == "SiteUserForm")
            {
                if (id == 0)
                {
                    ViewData["Title"] = $"New User";
                    ViewData["FormType"] = $"New User";
                }
                else
                {
                    ViewData["Title"] = $"Edit User";
                    ViewData["FormType"] = $"Edit User";

                   // var data = _patientApiService.SearchPatient(id);
                    return View(view);
                }
            }
            else if (view == "Index")
            {
                ViewData["Title"] = $"Search Users";
            }

            return View(view);
        }
    }
}