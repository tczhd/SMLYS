using SMLYS.ApplicationCore.DTOs.Taxes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.Services.Taxes
{
    public interface ITaxService
    {
        List<TaxModel> SearchTaxesAsync(int countryId, int? regionId, bool includeInActive);
    }
}
