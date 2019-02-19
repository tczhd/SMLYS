using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.UserAggregate
{
    public class AspNetUser : IdentityUser
    {
        public virtual ICollection<SiteUser> SiteUsers { get; set; }
    }
}
