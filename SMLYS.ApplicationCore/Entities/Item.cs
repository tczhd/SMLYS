using System;
using System.Collections.Generic;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Entities.UserAggregate;


namespace SMLYS.ApplicationCore.Entities
{
    public partial class Item : BaseEntity
    {
        public Item()
        {
            InvoiceItem = new HashSet<InvoiceItem>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public bool Active { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public int UpdatedBy { get; set; }
        public int ClinicId { get; set; }
        public int? ServiceGroupId {get;set;}
        public string ShortCode { get; set; }
        public int? IndustryCodeId { get; set; }
        public bool Subscription { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual SiteUser UpdatedByNavigation { get; set; }
        public virtual ServiceGroup ServiceGroup { get; set; }
        public virtual IndustryCode IndustryCode { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }
    }
}
