﻿using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Specifications.Patients
{
    public class PatientSpecification : BaseSpecification<Patient>
    {
        public PatientSpecification(string name) : base(p => p.FirstName == name)
        {
            AddInclude(b => b.Address);
            AddInclude($"{nameof(Patient.Address)}.{nameof(Address.Country)}");
            AddInclude($"{nameof(Patient.Address)}.{nameof(Address.RegionNavigation)}");
        }

        public void AddFirstName(string firstName)
        {
            AddInclude(q => q.FirstName == firstName);
        }
    }
}
