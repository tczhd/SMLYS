using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.User
{
    public class SiteUserModel
    {
        public int SiteUserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsDoctor { get; set; }
    }
}
