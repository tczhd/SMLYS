using SMLYS.ApplicationCore.DTOs.Doctors;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.Services.Doctor
{
    public interface IDoctorService
    {
        List<DoctorModel> SearchDoctorsAsync(int clinicId);
    }
}
