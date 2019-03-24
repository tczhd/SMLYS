using SMLYS.ApplicationCore.Entities.UserAggregate;
using SMLYS.ApplicationCore.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Specifications.Users
{
    public class SearchSiteUserSpecification : BaseSpecification<SiteUser>
    {
        public SearchSiteUserSpecification() : base()
        {
            AddInclude(b => b.SiteUserLevel);
        }
    }
}
