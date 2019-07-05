using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.User;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMLYS.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserHandler _userHandler;
        public BaseController(UserHandler userHandler)
        {
            _userHandler = userHandler;
            var userContext = _userHandler.GetUserContext();
        }

        public UserContext GetUserContext()
        {
            return _userHandler.GetUserContext();
        }

        public IActionResult GetActionResult()
        {
            var userContext = _userHandler.GetUserContext();
            if(userContext == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            return null;
        }
    }
}
