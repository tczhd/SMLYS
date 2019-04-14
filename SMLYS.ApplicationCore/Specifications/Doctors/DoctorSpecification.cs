using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Specifications.Doctors
{
    public class DoctorSpecification : BaseSpecification<Doctor>
    {
        public DoctorSpecification(int clinicId) : base()
        {
            AddCriteria(q => q.ClinicId == clinicId);
        }

    }
}
