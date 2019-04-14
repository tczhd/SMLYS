using SMLYS.ApplicationCore.DTOs.Doctors;
using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Doctor;
using SMLYS.ApplicationCore.Specifications.Doctors;
using System.Collections.Generic;
using System.Linq;

namespace SMLYS.ApplicationCore.Services.Doctors
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _doctorRepository;

        public DoctorService(IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public List<DoctorModel> SearchDoctorsAsync(int clinicId)
        {
            var doctorSpecification = new DoctorSpecification(clinicId);

            var data = _doctorRepository.List(doctorSpecification);

            var result = data.Select(p => (DoctorModel)p).ToList();

            return result;
        }
    }
}
