using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Interfaces.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Patients
{
    public class PatientModel : IResultable<Patient, PatientModel>
    {
        public int PatientId { get; set; }
        public int FamilyId { get; set; }
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel Address { get; set; }

        public Expression<Func<Patient, PatientModel>> CreateResult()
        {
            return m => new PatientModel
            {
                DoctorId = m.DoctorId,
                FamilyId = m.FamilyId,
                PatientId = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Address = new AddressModel {
                    Address1 = m.Address.Address1,
                    Address2 = m.Address.Address2,
                    AttentionTo = m.Address.AttentionTo,
                    City = m.Address.City,
                    CountryId = m.Address.CountryId,
                    PostalCode = m.Address.PostalCode,
                    RegionId = m.Address.RegionId,
                    RegionName = m.Address.RegionNavigation.Name,
                    CountryName = m.Address.Country.Iso2
                }
            };
        }

        public static implicit operator PatientModel(Patient source)
        {
            return new PatientModel
            {
                DoctorId = source.DoctorId,
                FamilyId = source.FamilyId,
                PatientId = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Address = new AddressModel
                {
                    Address1 = source.Address.Address1,
                    Address2 = source.Address.Address2,
                    AttentionTo = source.Address.AttentionTo,
                    City = source.Address.City,
                    CountryId = source.Address.CountryId,
                    PostalCode = source.Address.PostalCode,
                    RegionId = source.Address.RegionId,
                    RegionName = source.Address.RegionNavigation.Name,
                    CountryName = source.Address.Country.Iso2
                }
            };
        }

        public static implicit operator Patient(PatientModel source)
        {
            return new Patient
            {
                DoctorId = source.DoctorId,
                FamilyId = source.FamilyId,
                Id = source.PatientId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Address = new Address
                {
                    Address1 = source.Address.Address1,
                    Address2 = source.Address.Address2,
                    AttentionTo = source.Address.AttentionTo,
                    City = source.Address.City,
                    CountryId = source.Address.CountryId,
                    PostalCode = source.Address.PostalCode,
                    RegionId = source.Address.RegionId
                }
            };
        }
    }
}
