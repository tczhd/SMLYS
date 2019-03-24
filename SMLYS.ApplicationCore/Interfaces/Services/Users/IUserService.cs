using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.User;
using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Interfaces.Services.Users
{
    public interface IUserService
    {
        UserContext GetUserContextAsync(string userId);

        Result RegisterUser(SiteUserModel siteUserModel);

        List<SiteUserModel> SearchSiteUsers();
    }
}
