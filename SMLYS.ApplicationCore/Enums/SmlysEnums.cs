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

    public enum PaymentType
    {
        [Description("Cash")]
        Cash = 1,
        [Description("Check")]
        Check = 2,
        [Description("CreditCard")]
        CreditCard = 2,
    }

    public enum CreditCardType
    {
        [Description("Visa")]
        Visa = 1,
        [Description("MasterCard")]
        MasterCard = 2,
        [Description("American Express")]
        AmericanExpress = 2,
    }

    public enum PaymentGateWayType
    {
        [Description("HelCim")]
        HelCim= 1
    }
}
