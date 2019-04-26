using SMLYS.ApplicationCore.Entities.CommonAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public Expression<Func<Address, AddressModel>> CreateResult()
        {
            return m => new AddressModel
            {
                Address1 = m.Address1,
                Address2 = m.Address2,
                AttentionTo = m.AttentionTo,
                City = m.City,
                CountryId = m.CountryId,
                CountryName = m.Country.Iso2,
                RegionId = m.RegionId,
              //  RegionName = m.RegionNavigation.Name,
                PostalCode = m.PostalCode

            };
        }

        public static implicit operator AddressModel(Address source)
        {
            return new AddressModel
            {
                Address1 = source.Address1,
                Address2 = source.Address2,
                AttentionTo = source.AttentionTo,
                City = source.City,
                CountryId = source.CountryId,
                CountryName = source.Country.Iso2,
                RegionId = source.RegionId,
                //RegionName = source.RegionNavigation.Name,
                PostalCode = source.PostalCode
            };
        }

        public static implicit operator Address(AddressModel source)
        {
            return new Address
            {
                Address1 = source.Address1,
                Address2 = source.Address2,
                AttentionTo = source.AttentionTo,
                City = source.City,
                CountryId = source.CountryId,
                RegionId = source.RegionId,
                PostalCode = source.PostalCode
            };
        }
    }
}
