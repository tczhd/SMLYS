using System.Collections.Generic;

namespace SMLYS.ApplicationCore.Entities.UserAggregate
{
    public partial class SiteUserLevel
    {
        public SiteUserLevel()
        {
            SiteUser = new HashSet<SiteUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SiteUser> SiteUser { get; set; }
    }
}
