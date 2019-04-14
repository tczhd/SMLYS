using SMLYS.ApplicationCore.DTOs.Taxes;
using SMLYS.ApplicationCore.Entities.SettingsAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Taxes;
using SMLYS.ApplicationCore.Specifications.Taxes;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Services.Taxes
{
    public class TaxService : ITaxService
    {

        private readonly IRepository<Tax> _taxRepository;

        public TaxService(IRepository<Tax> taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public List<TaxModel> SearchTaxesAsync(int countryId, int? regionId, bool includeInActive)
        {
            var taxSpecification = new TaxSpecification();
            taxSpecification.AddCountryId(countryId);
            if (regionId != null)
            {
                taxSpecification.AddRegionId((int)regionId);
            }
            if (!includeInActive)
            {
                taxSpecification.AddActiveOnly();
            }

            var data = _taxRepository.List(taxSpecification);

            var result = data.Select(p => (TaxModel)p).ToList();

            return result;


        }
    }
}
