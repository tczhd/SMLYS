using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.User
{
    public class UserContext
    {
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int SiteUserId { get; set; }
        public string SiteUserName { get; set; }
        public int SiteUserLevelId { get; set; }
        public string SiteUserLevelName { get; set; }
        public string SiteUserImage{ get; set; }
        public int? DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int ClinicCountryId { get; set; }
        public int? ClinicRegionId { get; set; }
    }
}
