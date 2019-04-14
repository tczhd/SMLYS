using SMLYS.ApplicationCore.Entities.SettingsAggregate;
using SMLYS.ApplicationCore.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Specifications.Taxes
{
    public class TaxSpecification : BaseSpecification<Tax>
    {
        public TaxSpecification() : base()
        {
        }

        public void AddCountryId(int countryId)
        {
            AddCriteria(q => q.CountryId == countryId);
        }

        public void AddRegionId(int regionId)
        {
            AddCriteria(q => q.RegionId == regionId);
        }

        public void AddActiveOnly()
        {
            AddCriteria(q => q.Active);
        }
    }
}
