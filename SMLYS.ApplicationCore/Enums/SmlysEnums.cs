using System.ComponentModel;


namespace SMLYS.ApplicationCore.Enums
{
    public enum SiteUserLevelType
    {
        [Description("AdminUser")]
        AdminUser = 1,
        [Description("User")]
        User = 2
    }

    public enum PaymentGateWayType
    {
        [Description("HelCim")]
        HelCim= 1
    }
}
