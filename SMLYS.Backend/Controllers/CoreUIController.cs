﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SMLYS.Backend.Controllers
{
    [Authorize]
    [Route("CoreUI")]
    public class CoreUIController : Controller
    {
        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = "CoreUI - Open Source Bootstrap Admin Template";

            return View(view);
        }
    }
}
