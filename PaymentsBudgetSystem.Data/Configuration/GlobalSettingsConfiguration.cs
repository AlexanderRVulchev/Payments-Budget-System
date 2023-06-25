using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsBudgetSystem.Data.Entities;

namespace PaymentsBudgetSystem.Data.Configuration
{
    public class GlobalSettingsConfiguration : IEntityTypeConfiguration<GlobalSetting>
    {
        public void Configure(EntityTypeBuilder<GlobalSetting> builder)
        {
            builder.HasData(SeedGlobalSettings());
        }

        private List<GlobalSetting> SeedGlobalSettings()
            => new List<GlobalSetting>()
            {
                new GlobalSetting
                {
                    Id = 1,
                    SettingName = "Стопански инвентар - полезен живот в месеци",
                    SettingValue = 180
                },
                new GlobalSetting
                {
                    Id = 2,
                    SettingName = "Стопански инвентар - процент остатъчна стойност",
                    SettingValue = 0.1m
                },
                new GlobalSetting
                {
                    Id = 3,
                    SettingName = "Техника и оборудване - полезен живот в месеци",
                    SettingValue = 60
                },
                new GlobalSetting
                {
                    Id = 4,
                    SettingName = "Техника и оборудване - процент остатъчна стойност",
                    SettingValue = 0.15m
                },
                new GlobalSetting
                {
                    Id = 5,
                    SettingName = "Нематериални активи - полезен живот в месеци",
                    SettingValue = 12
                },
                new GlobalSetting
                {
                    Id = 6,
                    SettingName = "Нематериални активи - процент остатъчна стойност",
                    SettingValue = 0
                },
            };
            
        
    }
}
