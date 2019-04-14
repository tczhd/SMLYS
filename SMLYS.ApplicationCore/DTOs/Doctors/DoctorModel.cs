using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Interfaces.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Doctors
{
    public class DoctorModel: IResultable<Doctor, DoctorModel>
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Expression<Func<Doctor, DoctorModel>> CreateResult()
        {
            return m => new DoctorModel
            {
                DoctorId = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName
            };
        }

        public static implicit operator DoctorModel(Doctor source)
        {
            return new DoctorModel
            {
                DoctorId = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName
            };
        }

        public static implicit operator Doctor(DoctorModel source)
        {
            return new Doctor
            {
                Id = source.DoctorId,
                FirstName = source.FirstName,
                LastName = source.LastName
            };
        }
    }
}
