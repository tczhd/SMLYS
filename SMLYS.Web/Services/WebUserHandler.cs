using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.User;
using SMLYS.Web.Interfaces;

namespace SMLYS.Web.Services
{
    public class WebUserHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebUserHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserContext GetUserContext()
        {
            var userHandler = new UserHandler(_httpContextAccessor);
            var userContext = userHandler.GetUserContext();
            //if (userContext == null)
            //{
            //    _httpContextAccessor.HttpContext.Response.Redirect("/Identity/Account/Login");
            //}

            return userContext;
        }

    }
}
