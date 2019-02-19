using SMLYS.ApplicationCore.Entities.SettingsAggregate;


namespace SMLYS.ApplicationCore.Entities.DoctorAggregate
{
    public partial class DoctorSpecality
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int SpecalityId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Specality Specality { get; set; }
    }
}
