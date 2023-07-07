using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Administration
{
    using static Common.DataConstants.GlobalSetting;
    using static Common.ValidationErrors.General;

    public class GlobalSettingsEditModel
    {
        [Display(Name = "Стопански инвентар - полезен живот в месеци")]
        [Range(typeof(decimal), SettingMinValue, AssetLifeMaxValue, ErrorMessage = RangeValidationError)]
        public decimal UpkeepLife { get; set; }

        [Display(Name = "Стопански инвентар - процент остатъчна стойност")]
        [Range(typeof(decimal), SettingMinValue, ResidualPartPercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal UpkeepResidualPartPercentage { get; set; }

        [Display(Name = "Техника и оборудване - полезен живот в месеци")]
        [Range(typeof(decimal), SettingMinValue, AssetLifeMaxValue, ErrorMessage = RangeValidationError)]
        public decimal TangibleAssetLife { get; set; }

        [Display(Name = "Техника и оборудване - процент остатъчна стойност")]
        [Range(typeof(decimal), SettingMinValue, ResidualPartPercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal TangibleAssetResidualPartPercentage { get; set; }

        [Display(Name = "Нематериални активи - полезен живот в месеци")]
        [Range(typeof(decimal), SettingMinValue, AssetLifeMaxValue, ErrorMessage = RangeValidationError)]
        public decimal IntangibleAssetLife { get; set; }

        [Display(Name = "Нематериални активи - процент остатъчна стойност")]
        [Range(typeof(decimal), SettingMinValue, ResidualPartPercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal IntangibleAssetResidualPartPercentage { get; set; }

        [Display(Name = "Фонд Пенсии - работодател")]
        [Range(typeof(decimal), SettingMinValue, InsurancePercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal InsurancePensionEmployerPercentage { get; set; }

        [Display(Name = "Фонд Пенсии - служител")]
        [Range(typeof(decimal), SettingMinValue, InsurancePercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal InsurancePensionEmployeePercentage { get; set; }

        [Display(Name = "Здравно осигуряване - работодател")]
        [Range(typeof(decimal), SettingMinValue, InsurancePercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal HealthInsuranceEmployerPercentage { get; set; }

        [Display(Name = "Здравно осигуряване - служител")]
        [Range(typeof(decimal), SettingMinValue, InsurancePercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal HealthInsuranceEmployeePercentage { get; set; }

        [Display(Name = "Oсигуряване в УПФ - работодател")]
        [Range(typeof(decimal), SettingMinValue, InsurancePercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal AdditionalInsuranceEmployerPercentage { get; set; }

        [Display(Name = "Oсигуряване в УПФ - служител")]
        [Range(typeof(decimal), SettingMinValue, InsurancePercentageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal AdditionalInsuranceEmployeePercentage { get; set; }

        [Display(Name = "Данък общ доход")]
        [Range(typeof(decimal), SettingMinValue, TaxRateMaxValue, ErrorMessage = RangeValidationError)]
        public decimal TaxRate { get; set; }

        [Display(Name = "Минимална работна заплата")]
        [Range(typeof(decimal), SettingMinValue, MinimumWageMaxValue, ErrorMessage = RangeValidationError)]
        public decimal MinimumWage { get; set; }
    }
}
