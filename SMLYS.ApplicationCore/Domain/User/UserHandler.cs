
using SMLYS.ApplicationCore.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
using SMLYS.ApplicationCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace SMLYS.ApplicationCore.Domain.User
{
    public class UserHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserContext GetUserContext()
        {
            var userContext =  _httpContextAccessor.HttpContext.Session.Get<UserContext>("UserContext");
            return userContext;
        }

        public void SetUserContext(UserContext userContext)
        {
            _httpContextAccessor.HttpContext.Session.Set("UserContext", userContext);
        }
    }
}
