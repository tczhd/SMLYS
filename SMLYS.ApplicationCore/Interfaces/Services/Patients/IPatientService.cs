﻿using SMLYS.ApplicationCore.Entities.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLYS.ApplicationCore.Interfaces.Services.Patients
{
    public interface IPatientService
    {
        List<Patient> CreatePatientAsync(List<Patient> patient);
    }
}
