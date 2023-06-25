namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Assets;
    using Models.Enums;
    using PaymentsBudgetSystem.Data.Entities.Enums;

    public class AssetService : IAssetService
    {
        private readonly PBSystemDbContext context;

        public AssetService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<List<AssetInfoViewModel>> GetAllAssetsAsync(string userId, int year, int month)
        {
            var settings = await context
                .GlobalSettings
                .Select(gs => new GlobalSettingDataModel
                {
                    Id = (GlobalSetting)gs.Id,
                    SettingName = gs.SettingName,
                    SettingValue = gs.SettingValue
                })
                .ToListAsync();

            var typeTexts = new Dictionary<int, string>
            {
                { 9, "Стопански инвентар" },
                { 10, "Техника и оборудване" },
                { 11, "Нематериални активи" }
            };

            var assets = await context
                .Assets
                .Where(a => a.UserId == userId &&
                    (a.DateAquired.Year < year ||
                    a.DateAquired.Year == year && a.DateAquired.Month <= month))
                .Select(a => new AssetInfoViewModel
                {
                    AssetId = a.Id,
                    Name = a.Description,
                    DateAquired = a.DateAquired,
                    DateDisposed = a.DateDisposed,
                    ReportValue = a.ReportValue,
                    TypeText = typeTexts[(int)a.Type],
                    Type = a.Type,
                    PaymentId = a.PaymentAssetDetailsId
                })
                .ToListAsync();

            foreach (var asset in assets)
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

                asset.ResidualValue = assetResidualValue;
                asset.BalanceValue = assetBalanceValue;
                asset.Amortization = assetAmortization;
            }

            return assets;
        }
    }
}
