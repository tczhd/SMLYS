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
        CreditCard = 3,
    }

    public enum PaymentMethodType
    {
        [Description("Visa")]
        Visa = 1,
        [Description("MasterCard")]
        MasterCard = 2,
        [Description("American Express")]
        AmericanExpress = 3,
    }

    public enum PaymentStatusType
    {
        [Description("Purchase")]
        Purchase = 1,
        [Description("Void")]
        Void = 2,
        [Description("Refund")]
        Refund = 3,
    }

    public enum PaymentGateWayType
    {
        [Description("HelCim")]
        HelCim= 1
    }
}
