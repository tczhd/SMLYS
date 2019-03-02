
namespace SMLYS.ApplicationCore.Entities.SettingsAggregate
{
    public partial class Tax : BaseEntity
    {
        public string TaxName { get; set; }
        public decimal TaxRate { get; set; }
        public int? RegionId { get; set; }
        public int CountryId { get; set; }
        public bool Active { get; set; }
    }
}
