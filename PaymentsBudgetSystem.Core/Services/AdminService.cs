using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Data;
    using Models;
    using Core.Models.Administration;
    using GlobalSetting = Models.Enums.GlobalSetting;

    public class AdminService : IAdminService
    {
        private readonly PBSystemDbContext context;

        public AdminService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<GlobalSettingsEditModel> GetGlobalSettingsAsync()
        {
            var settings = await context
                 .GlobalSettings
                 .OrderBy(gs => gs.Id)
                 .ToArrayAsync();

            var model = new GlobalSettingsEditModel
            {
                UpkeepLife = settings[(int)GlobalSetting.UpkeepLife - 1].SettingValue,
                UpkeepResidualPartPercentage = settings[(int)GlobalSetting.UpkeepResidualPart - 1].SettingValue * 100,
                TangibleAssetLife = settings[(int)GlobalSetting.TangibleAssetLife - 1].SettingValue,
                TangibleAssetResidualPartPercentage = settings[(int)GlobalSetting.TangibleAssetResidualPart - 1].SettingValue * 100,
                IntangibleAssetLife = settings[(int)GlobalSetting.IntangibleAssetLife - 1].SettingValue,
                IntangibleAssetResidualPartPercentage = settings[(int)GlobalSetting.IntangibleAssetResidualPart - 1].SettingValue * 100,
                InsurancePensionEmployerPercentage = settings[(int)GlobalSetting.InsurancePensionEmployer - 1].SettingValue * 100,
                InsurancePensionEmployeePercentage = settings[(int)GlobalSetting.InsurancePensionEmployee - 1].SettingValue * 100,
                AdditionalInsuranceEmployerPercentage = settings[(int)GlobalSetting.AdditionalInsuranceEmployer - 1].SettingValue * 100,
                AdditionalInsuranceEmployeePercentage = settings[(int)GlobalSetting.AdditionalInsuranceEmployee - 1].SettingValue * 100,
                HealthInsuranceEmployerPercentage = settings[(int)GlobalSetting.HealthInsuranceEmployer - 1].SettingValue * 100,
                HealthInsuranceEmployeePercentage = settings[(int)GlobalSetting.HealthInsuranceEmployee - 1].SettingValue * 100,
                TaxRate = settings[(int)GlobalSetting.TaxRate - 1].SettingValue * 100,
                MinimumWage = settings[(int)GlobalSetting.MinimumWage - 1].SettingValue,            
            };

            return model;
        }

        public async Task SaveGlobalSettingsAsync(GlobalSettingsEditModel model)
        {
            var settings = await context
                .GlobalSettings
                .OrderBy(gs => gs.Id)
                .ToArrayAsync();

            settings[(int)GlobalSetting.UpkeepLife - 1].SettingValue = model.UpkeepLife;
            settings[(int)GlobalSetting.UpkeepResidualPart - 1].SettingValue = model.UpkeepResidualPartPercentage / 100;
            settings[(int)GlobalSetting.TangibleAssetLife - 1].SettingValue = model.TangibleAssetLife;
            settings[(int)GlobalSetting.TangibleAssetResidualPart - 1].SettingValue = model.TangibleAssetResidualPartPercentage / 100;
            settings[(int)GlobalSetting.IntangibleAssetLife - 1].SettingValue = model.IntangibleAssetLife;
            settings[(int)GlobalSetting.IntangibleAssetResidualPart - 1].SettingValue = model.IntangibleAssetResidualPartPercentage / 100;
            settings[(int)GlobalSetting.InsurancePensionEmployer - 1].SettingValue = model.InsurancePensionEmployerPercentage / 100;
            settings[(int)GlobalSetting.InsurancePensionEmployee - 1].SettingValue = model.InsurancePensionEmployeePercentage / 100;
            settings[(int)GlobalSetting.AdditionalInsuranceEmployer - 1].SettingValue = model.AdditionalInsuranceEmployerPercentage / 100;
            settings[(int)GlobalSetting.AdditionalInsuranceEmployee - 1].SettingValue = model.AdditionalInsuranceEmployeePercentage / 100;
            settings[(int)GlobalSetting.HealthInsuranceEmployer - 1].SettingValue = model.HealthInsuranceEmployerPercentage / 100;
            settings[(int)GlobalSetting.HealthInsuranceEmployee - 1].SettingValue = model.HealthInsuranceEmployeePercentage / 100;
            settings[(int)GlobalSetting.TaxRate - 1].SettingValue = model.TaxRate / 100;
            settings[(int)GlobalSetting.MinimumWage - 1].SettingValue = model.MinimumWage;

            await context.SaveChangesAsync();
        }
    }
}
