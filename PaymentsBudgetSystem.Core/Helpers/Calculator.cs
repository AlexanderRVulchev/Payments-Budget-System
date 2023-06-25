namespace PaymentsBudgetSystem.Core.Helpers
{
    using Models;
    using Models.Assets;
    using Models.Enums;
    using Data.Entities.Enums;

    public static class Calculator
    {
        public static AssetInfoViewModel CalculateAssetDataByYearAndMonth(
            int year,
            int month,
            AssetInfoViewModel asset,
            List<GlobalSettingDataModel> settings)
        {
            int numberOfMonthsSinceAquisition = (year - asset.DateAquired.Year) * 12 + (month - asset.DateAquired.Month);
            int totalLifeOfAssetInMonths = default;
            decimal residualValuePart = default;

            if (asset.Type == ParagraphType.UpkeepLongTermAssets5100)
            {
                residualValuePart = settings.First(s => s.Id == GlobalSetting.UpkeepResidualPart).SettingValue;
                totalLifeOfAssetInMonths = (int)settings.First(s => s.Id == GlobalSetting.UpkeepLife).SettingValue;
            }
            else if (asset.Type == ParagraphType.AquisitionLongTermAssets5200)
            {
                residualValuePart = settings.First(s => s.Id == GlobalSetting.TangibleAssetResidualPart).SettingValue;
                totalLifeOfAssetInMonths = (int)settings.First(s => s.Id == GlobalSetting.TangibleAssetLife).SettingValue;
            }
            else if (asset.Type == ParagraphType.AquisitionIntangibleAssets5300)
            {
                residualValuePart = settings.First(s => s.Id == GlobalSetting.IntangibleAssetResidualPart).SettingValue;
                totalLifeOfAssetInMonths = (int)settings.First(s => s.Id == GlobalSetting.IntangibleAssetLife).SettingValue;
            }

            decimal assetResidualValue = residualValuePart * asset.ReportValue;
            decimal totalAmortizationQuota = asset.ReportValue - assetResidualValue;
            decimal amortizationPerMonth = totalAmortizationQuota / totalLifeOfAssetInMonths;
            decimal assetAmortization = amortizationPerMonth * numberOfMonthsSinceAquisition;
            decimal assetBalanceValue = asset.ReportValue - assetAmortization;

            if (assetAmortization > totalAmortizationQuota)
            {
                assetAmortization = asset.ReportValue - assetResidualValue;
                assetBalanceValue = asset.ReportValue - assetAmortization;
            }

            asset.ResidualValue = assetResidualValue;
            asset.BalanceValue = assetBalanceValue;
            asset.Amortization = assetAmortization;
            


            return asset;
        } 
    }
}
