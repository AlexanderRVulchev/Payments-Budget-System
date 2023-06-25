
namespace PaymentsBudgetSystem.Core.Models
{
    using Enums;

    public class GlobalSettingDataModel
    {
        public GlobalSetting Id { get; set; }

        public string SettingName { get; set; } = null!;

        public decimal SettingValue { get; set; }
    }
}
