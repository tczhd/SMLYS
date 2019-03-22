using SMLYS.ApplicationCore.Entities.UserAggregate;
using SMLYS.ApplicationCore.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Specifications.Users
{
    public class UserContextSpefification : BaseSpecification<SiteUser>
    {
        public UserContextSpefification() : base()
        {
            AddInclude(b => b.SiteUserLevel);
            AddInclude(b => b.Clinic);
            AddInclude(b => b.Doctor);
        }

        public void AddUserId(string userId)
        {
            AddCriteria(q => q.UserId == userId);
        }
    }
}
