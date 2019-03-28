using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.Entities.SettingsAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Utiliites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMLYS.ApplicationCore.Services.Utiliites
{
    public class UtilityService : IUtilityService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Region> _regionRepository;

        public UtilityService(IRepository<Country> countryRepository, IRepository<Region> regionRepository)
        {
            _countryRepository = countryRepository;
            _regionRepository = regionRepository;
        }
        public List<ListItemModel> GetCountries()
        {
            var data = _countryRepository.ListAll().Select(p => new ListItemModel {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return data;
        }

        public List<ListItemModel> GetRegions()
        {
            var data = _regionRepository.ListAll().Select(p => new ListItemModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return data;
        }
    }
}
