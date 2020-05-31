using System.ComponentModel;

namespace Domain.People.Customers
{
    public enum CustomerType
    {
        [Description("Normal")]
        Normal = 0,

        [Description("Coparticição")]
        Copartiction = 1,

        [Description("Convênio")]
        HealthInsurance = 2,
    }
}
