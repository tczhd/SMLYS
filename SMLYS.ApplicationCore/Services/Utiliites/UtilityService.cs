using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.Entities;
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
        private readonly IRepository<ServiceGroup> _serviceGroupRepository;
        private readonly IRepository<IndustryCode> _inventoryCodeRepository;
        public UtilityService(IRepository<Country> countryRepository
            , IRepository<Region> regionRepository
            , IRepository<ServiceGroup> serviceGroupRepository
            , IRepository<IndustryCode> inventoryCodeRepository)
        {
            _countryRepository = countryRepository;
            _regionRepository = regionRepository;
            _serviceGroupRepository = serviceGroupRepository;
            _inventoryCodeRepository = inventoryCodeRepository;
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

        public List<ListItemModel> GetServiceGroups()
        {
            var data = _serviceGroupRepository.ListAll().Select(p => new ListItemModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return data;
        }

        public List<ListItemModel> GetIndustryCodes()
        {
            var data = _inventoryCodeRepository.ListAll().Select(p => new ListItemModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return data;
        }
    }
}
