using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Specifications.Patients
{
    public class PatientSpecification : BaseSpecification<Patient>
    {
        public PatientSpecification() : base()
        {
            AddInclude(b => b.Address);
            AddInclude($"{nameof(Patient.Address)}.{nameof(Address.Country)}");
            AddInclude($"{nameof(Patient.Address)}.{nameof(Address.RegionNavigation)}");
        }

        public void AddPatientId(int patientId)
        {
            AddCriteria(q => q.Id == patientId);
        }

        public void AddFirstName(string firstName)
        {
            AddCriteria(q => q.FirstName == firstName);
        }

        public void AddLastName(string lastName)
        {
            AddCriteria(q => q.LastName == lastName);
        }
    }
}
