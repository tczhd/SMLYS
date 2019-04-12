using SMLYS.ApplicationCore.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Patients
{
    public class PatientModel
    {
        public int PatientId { get; set; }
        public int FamilyId { get; set; }
        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public AddressModel Address { get; set; }
    }
}
