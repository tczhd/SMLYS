using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.User;

namespace SMLYS.Web.Interfaces
{
    public interface IWebUserHandler
    {
        UserContext GetUserContext();
    }
}
