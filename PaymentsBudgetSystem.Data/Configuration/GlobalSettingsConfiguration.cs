using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsBudgetSystem.Data.Configuration
{
    using Entities;

    public class GlobalSettingsConfiguration : IEntityTypeConfiguration<GlobalSetting>
    {
        public void Configure(EntityTypeBuilder<GlobalSetting> builder)
        {
            builder.HasData(SeedGlobalSettings());
        }

        private static List<GlobalSetting> SeedGlobalSettings()
            => new()
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
                new GlobalSetting
                {
                    Id = 7,
                    SettingName = "Фонд Пенсии - работодател",
                    SettingValue = 0.1372m,
                },
                new GlobalSetting
                {
                    Id = 8,
                    SettingName = "Фонд Пенсии - служител",
                    SettingValue = 0.1058m
                },
                new GlobalSetting
                {
                    Id = 9,
                    SettingName = "Здравно осигуряване - работодател",
                    SettingValue = 0.048m
                },
                new GlobalSetting
                {
                    Id = 10,
                    SettingName = "Здравно осигуряване - служител",
                    SettingValue = 0.032m
                },
                new GlobalSetting
                {
                    Id = 11,
                    SettingName = "Oсигуряване в УПФ - работодател",
                    SettingValue = 0.028m
                },
                new GlobalSetting
                {
                    Id=12,
                    SettingName = "Oсигуряване в УПФ - служител",
                    SettingValue = 0.022m,
                },
                new GlobalSetting
                {
                    Id=13,
                    SettingName = "Данък общ доход",
                    SettingValue = 0.1m,
                },
                new GlobalSetting
                {
                    Id = 14,
                    SettingName = "Минимална работна заплата",
                    SettingValue = 780m
                }
            };
    }
}
