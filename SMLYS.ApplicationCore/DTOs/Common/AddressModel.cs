using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Common
{
    public class AddressModel
    {
        public string AddressType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }
        public int? RegionId { get; set; }
        public string RegionName { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public string PostalCode { get; set; }

        public string AttentionTo { get; set; }
    }
}
