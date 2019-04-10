﻿using SMLYS.ApplicationCore.DTOs.Patients;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Patients;
using SMLYS.ApplicationCore.Specifications.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLYS.ApplicationCore.Services.Patients
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientService(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public List<Patient> CreatePatientAsync(List<Patient> patients)
        {
            try
            {
                var family = patients.First().Family;

                foreach (Patient patient in patients)
                {
                    if (patient.Id > 0)
                    {
                        var patientEntity = SearchPatientAsync(patient.Id);
                        family = patientEntity.Family;

                        patientEntity = CopyPatientData(patientEntity, patient);
                        _patientRepository.UpdateOnly(patientEntity);
                    }
                    else
                    {
                        patient.Family = family;
                        _patientRepository.AddOnly(patient);
                    }
                }

               _patientRepository.SaveAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Add patient failed: " + ex.Message);
            }

            return patients;
        }

        private Patient CopyPatientData(Patient patient, Patient soruce)
        {
            patient.Address.Address1 = soruce.Address.Address1;
            patient.Address.Address2 = soruce.Address.Address2;
            patient.Address.City = soruce.Address.City;
            patient.Address.CountryId = soruce.Address.CountryId;
            patient.Address.RegionId = soruce.Address.RegionId;
            patient.Address.Address1 = soruce.Address.Address1;

            patient.FirstName = soruce.FirstName;
            patient.LastName = soruce.LastName;
            patient.Phone = soruce.Phone;
            patient.Email = soruce.Email;

            return patient;
        }

        public List<Patient> SearchPatientAsync(List<SearchPatientParameter> searchPatientParameter)
        {
            var patientSpecification = new PatientSpecification();

            foreach (var parameter in searchPatientParameter)
            {
                if (parameter.SearchType == "first_name")
                {
                    patientSpecification.AddFirstName(parameter.SearchContent);
                }
                else if (parameter.SearchType == "last_name")
                {
                    patientSpecification.AddLastName(parameter.SearchContent);
                }
            }

            return  _patientRepository.List(patientSpecification).ToList();
        }

        public Patient SearchPatientAsync(int id)
        {
            var patientSpecification = new PatientSpecification();
            patientSpecification.AddPatientId(id);

            return _patientRepository.GetSingleBySpec(patientSpecification);
        }
    }
}
