using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Data;
    using Models;
    using Models.Assets;
    using Models.Enums;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Core.Helpers;

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

            foreach (var asset in assets)
            {
                Calculator.CalculateAssetDataByYearAndMonth(model.InfoYear, model.InfoMonth, asset, settings);               
            }

            model.Assets = Sorter.SortAssets(assets, model.SortAttribute, model.SortBy);

            return model;
        }
    }
}
