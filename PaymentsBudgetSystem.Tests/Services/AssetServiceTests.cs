using Microsoft.EntityFrameworkCore;
using PaymentsBudgetSystem.Core.Contracts;
using PaymentsBudgetSystem.Core.Models.Assets;
using PaymentsBudgetSystem.Core.Models.Enums;
using PaymentsBudgetSystem.Core.Services;
using PaymentsBudgetSystem.Data;
using PaymentsBudgetSystem.Data.Entities;
using System.Security.Permissions;
using GlobalSetting = PaymentsBudgetSystem.Data.Entities.GlobalSetting;

namespace PaymentsBudgetSystem.Tests.Services
{
    [TestFixture]
    public class AssetServiceTests
    {
        private PBSystemDbContext context;
        private IAssetService assetService;

        private string userId;

        [SetUp]
        public async Task SetUp()
        {
            userId = "test user id";

            var globalSettings = GetGlobalSettings();

            List<Asset> assets = new()
            {
                new Asset
                {
                    DateAquired = new DateTime(2023, 1, 1),
                    Description = "",
                    PaymentAssetDetailsId = Guid.Parse("a101e1f0-d46a-466f-9c98-56bfe65e4db3"),
                    Id = Guid.NewGuid(),
                    ReportValue = 1000,
                    Type = Data.Entities.Enums.ParagraphType.UpkeepLongTermAssets5100,
                    UserId = userId
                },
                new Asset
                {
                    DateAquired = new DateTime(2023, 2, 2),
                    Description = "",
                    PaymentAssetDetailsId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    ReportValue = 500,
                    Type = Data.Entities.Enums.ParagraphType.AquisitionLongTermAssets5200,
                    UserId = userId
                },
                new Asset
                {
                    DateAquired = new DateTime(2023, 3, 3),
                    Description = "",
                    PaymentAssetDetailsId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    ReportValue = 2000,
                    Type = Data.Entities.Enums.ParagraphType.AquisitionIntangibleAssets5300,
                    UserId = userId
                },
            };

            var paymentAssetDetails = new PaymentAssetsDetails
            {
                AssetPaymentId = Guid.Parse("a101e1f0-d46a-466f-9c98-56bfe65e4db3"),
                Beneficiary = new Beneficiary
                {
                    Address = "test address",
                    BankAccount = "test bank account",
                    Id = Guid.NewGuid(),
                    Identifier = "1234",
                    Name = "beneficiary name",
                    UserId = userId,
                },
                DeliveryDate = new DateTime(2023, 1, 1),
                InvoiceDate = new DateTime(2023, 1 ,1),
                InvoiceNumber = "1234",
                Payment = new Payment
                {
                    Id = Guid.Parse("a101e1f0-d46a-466f-9c98-56bfe65e4db3"),
                    Amount = 0,
                    Date = new DateTime(2023, 1, 1),
                    Description = string.Empty,
                    ReceiverName = string.Empty,
                    UserId = userId
                }
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                .Options;
            context = new PBSystemDbContext(options);

            context.Database.EnsureDeleted();

            await context.Assets.AddRangeAsync(assets);
            await context.GlobalSettings.AddRangeAsync(globalSettings);

            await context.SaveChangesAsync();

            assetService = new AssetService(context);
        }

        [Test]
        public async Task GetAllAssets_WorksAsIntended()
        {
            var result = await assetService.GetAllAssetsAsync(userId, new AllAssetsViewModel
            {
                InfoMonth = 11,
                InfoYear = 2023,
                NameFilter = null,
                NumberOfPages = 0,
                SortAttribute = AssetSort.Name,
                SortBy = SortBy.Ascending,
                Page = 0
            });

            var orderedAssets = context.Assets.OrderBy(a => a.Description);

            Assert.IsNotNull(result);
            Assert.That(result.Assets.Count, Is.EqualTo(3));
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.InfoYear, Is.EqualTo(2023));
            Assert.That(result.InfoMonth, Is.EqualTo(11));
            Assert.That(result.NameFilter, Is.EqualTo(null));
            Assert.That(result.SortAttribute, Is.EqualTo(AssetSort.Name));
            Assert.That(result.SortAttribute, Is.EqualTo(AssetSort.Name));
            Assert.That(result.Assets.Select(a => a.Name), Is.EqualTo(orderedAssets.Select(oa => oa.Description)));
        }

        [Test]
        public async Task GetAllAssets_CurrentPageShouldNotExceedNumberOfPages()
        {
            var result = await assetService.GetAllAssetsAsync(userId, new AllAssetsViewModel
            {
                InfoMonth = 11,
                InfoYear = 2023,
                NameFilter = null,
                NumberOfPages = 0,
                SortAttribute = AssetSort.Name,
                SortBy = SortBy.Ascending,
                Page = 5
            });

            Assert.That(result.Page, Is.EqualTo(1));
        }

        private static List<GlobalSetting> GetGlobalSettings()
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
