using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Data;
    using Models;
    using Models.Assets;
    using Models.Enums;
    using Core.Helpers;
    using Data.Entities;

    using static Common.ExceptionMessages.Asset;
    using GlobalSetting = Models.Enums.GlobalSetting;

    public class AssetService : IAssetService
    {
        private readonly PBSystemDbContext context;

        public AssetService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<AllAssetsViewModel> GetAllAssetsAsync(string userId, AllAssetsViewModel model)
        {
            var typeTexts = new Dictionary<int, string>
            {
                { 9, "Стопански инвентар" },
                { 10, "Техника и оборудване" },
                { 11, "Нематериални активи" }
            };

            var assets = await context
                .Assets
                .Where(a => a.UserId == userId &&
                    (a.DateAquired.Year < model.InfoYear ||
                    a.DateAquired.Year == model.InfoYear && a.DateAquired.Month <= model.InfoMonth))
                .Where(a => a.Description.Contains(model.NameFilter ?? String.Empty))
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

            var settings = await context
                 .GlobalSettings
                 .Select(gs => new GlobalSettingDataModel
                 {
                     Id = (GlobalSetting)gs.Id,
                     SettingName = gs.SettingName,
                     SettingValue = gs.SettingValue
                 })
                 .ToListAsync();

            Calculator calculator = new();
            foreach (var asset in assets)
            {
                calculator.CalculateAssetDataByYearAndMonth(model.InfoYear, model.InfoMonth, asset, settings);
            }

            Sorter sorter = new();
            model.Assets = sorter.SortAssets(assets, model.SortAttribute, model.SortBy);

            return model;
        }

        public async Task<AssetDetailsViewModel> GetAssetDetailsAsync(string userId, Guid id, int year)
        {
            var entity = await context
                .Assets
                .Where(a => a.Id == id)
                .Include(a => a.PaymentAssetsDetails)
                .ThenInclude(pad => pad.Beneficiary)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new InvalidOperationException(InvalidAsset);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(AssetAccessDenied);
            }

            var settings = await context
                 .GlobalSettings
                 .Select(gs => new GlobalSettingDataModel
                 {
                     Id = (GlobalSetting)gs.Id,
                     SettingName = gs.SettingName,
                     SettingValue = gs.SettingValue
                 })
                 .ToListAsync();

            var monthlyInfoModels = new List<AssetInfoViewModel>();

            for (int month = 1; month <= 12; month++)
            {
                var assetInfoModel = new AssetInfoViewModel
                {
                    DateAquired = entity.DateAquired,
                    ReportValue = entity.ReportValue,
                    Type = entity.Type
                };

                Calculator calculator = new();

                if (entity.DateAquired.Year < year ||
                   (entity.DateAquired.Year == year && entity.DateAquired.Month <= month))
                {
                    assetInfoModel = calculator.CalculateAssetDataByYearAndMonth(year, month, assetInfoModel, settings);
                }

                monthlyInfoModels.Add(assetInfoModel);
            }

            return new AssetDetailsViewModel
            {
                AssetId = entity.Id,
                BeneficiaryId = entity.PaymentAssetsDetails.BeneficiaryId,
                BeneficiaryName = entity.PaymentAssetsDetails.Beneficiary.Name,
                DateAquired = entity.DateAquired,
                Name = entity.Description,
                ParagraphType = entity.Type,
                ReportValue = entity.ReportValue,
                Year = year,
                AssetMonthlyStatus = monthlyInfoModels
            };
        }
    }
}
