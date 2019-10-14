using SMLYS.ApplicationCore.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.Services.Utiliites
{
    public interface IUtilityService
    {
        List<ListItemModel> GetCountries();
        List<ListItemModel> GetRegions();
        List<ListItemModel> GetServiceGroups();
    }
}
