using SMLYS.ApplicationCore.Entities.SettingsAggregate;


namespace SMLYS.ApplicationCore.Entities.DoctorAggregate
{
    public partial class DoctorSpecality : BaseEntity
    {
        public int DoctorId { get; set; }
        public int SpecalityId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Specality Specality { get; set; }
    }
}
