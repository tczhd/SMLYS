using SMLYS.ApplicationCore.Entities.SettingsAggregate;
using SMLYS.ApplicationCore.Interfaces.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Taxes
{
    public class TaxModel : IResultable<Tax, TaxModel>
    {
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public decimal TaxRate { get; set; }
        public int? RegionId { get; set; }
        public int CountryId { get; set; }
        public bool Active{ get; set; }

        public Expression<Func<Tax, TaxModel>> CreateResult()
        {
            return m => new TaxModel
            {
               Active = m.Active,
               CountryId = m.CountryId,
               RegionId = m.RegionId,
               TaxId = m.Id,
               TaxName = m.TaxName,
               TaxRate = m.TaxRate
            };
        }

        public static implicit operator TaxModel(Tax source)
        {
            return new TaxModel
            {
                Active = source.Active,
                CountryId = source.CountryId,
                RegionId = source.RegionId,
                TaxId = source.Id,
                TaxName = source.TaxName,
                TaxRate = source.TaxRate
            };
        }

        public static implicit operator Tax(TaxModel source)
        {
            return new Tax
            {
                Active = source.Active,
                CountryId = source.CountryId,
                RegionId = source.RegionId,
                Id = source.TaxId,
                TaxName = source.TaxName,
                TaxRate = source.TaxRate
            };
        }
    }
}
