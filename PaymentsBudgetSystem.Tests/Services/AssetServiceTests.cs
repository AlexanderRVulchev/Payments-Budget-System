using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Tests.Services
{
    using Core.Contracts;
    using Core.Models.Assets;
    using Core.Models.Enums;
    using Core.Services;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;

    using GlobalSetting = PaymentsBudgetSystem.Data.Entities.GlobalSetting;
    using static Tests.GlobalSettingsTestSeeder;

    [TestFixture]
    internal class AssetServiceTests
    {
        private PBSystemDbContext context;
        private IAssetService assetService;

        private string testUserId;

        [SetUp]
        public async Task SetUp()
        {
            testUserId = "test user id";

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
                    UserId = testUserId
                },
                new Asset
                {
                    DateAquired = new DateTime(2023, 2, 2),
                    Description = "",
                    PaymentAssetDetailsId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    ReportValue = 500,
                    Type = Data.Entities.Enums.ParagraphType.AquisitionLongTermAssets5200,
                    UserId = testUserId
                },
                new Asset
                {
                    DateAquired = new DateTime(2023, 3, 3),
                    Description = "",
                    PaymentAssetDetailsId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    ReportValue = 2000,
                    Type = Data.Entities.Enums.ParagraphType.AquisitionIntangibleAssets5300,
                    UserId = testUserId
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
                    UserId = testUserId,
                },
                DeliveryDate = new DateTime(2023, 1, 1),
                InvoiceDate = new DateTime(2023, 1, 1),
                InvoiceNumber = "1234",
                Payment = new Payment
                {
                    Id = Guid.Parse("a101e1f0-d46a-466f-9c98-56bfe65e4db3"),
                    Amount = 0,
                    Date = new DateTime(2023, 1, 1),
                    Description = string.Empty,
                    ReceiverName = string.Empty,
                    UserId = testUserId
                }
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();

            await context.Assets.AddRangeAsync(assets);
            await context.PaymentAssetsDetails.AddAsync(paymentAssetDetails);
            await context.GlobalSettings.AddRangeAsync(globalSettings);

            await context.SaveChangesAsync();

            assetService = new AssetService(context);
        }

        [Test]
        public async Task GetAllAssets_WorksAsIntended()
        {
            var result = await assetService
                .GetAllAssetsAsync(testUserId, new AllAssetsViewModel
                {
                    InfoMonth = 11,
                    InfoYear = 2023,
                    NameFilter = null,
                    NumberOfPages = 0,
                    SortAttribute = AssetSort.Name,
                    SortBy = SortBy.Ascending,
                    Page = 0
                });

            var orderedAssets = context.Assets
                .OrderBy(a => a.Description);

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

            var result = await assetService
                .GetAllAssetsAsync(testUserId, new AllAssetsViewModel
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

        [Test]
        public async Task GetAssetDetails_WorksAsIntended()
        {
            var testAssetGuidId = context.Assets.First().Id;
            var testYear = 2023;

            var expected = new AssetDetailsViewModel
            {
                AssetId = testAssetGuidId,
                Year = testYear,
                DateAquired = new DateTime(2023, 1, 1),
                Name = String.Empty,
                BeneficiaryName = "beneficiary name",
                ParagraphType = ParagraphType.UpkeepLongTermAssets5100,
                ReportValue = 1000
            };

            var result = await assetService
                .GetAssetDetailsAsync(testUserId, testAssetGuidId, testYear);

            Assert.That(result.DateAquired, Is.EqualTo(expected.DateAquired));
            Assert.That(result.ReportValue, Is.EqualTo(expected.ReportValue));
            Assert.That(result.Year, Is.EqualTo(expected.Year));
            Assert.That(result.ParagraphType, Is.EqualTo(expected.ParagraphType));
            Assert.That(result.BeneficiaryName, Is.EqualTo(expected.BeneficiaryName));
            Assert.That(result.AssetId, Is.EqualTo(expected.AssetId));
            Assert.That(result.AssetMonthlyStatus.Count, Is.EqualTo(12));
            Assert.That(result.AssetMonthlyStatus.Last().ReportValue != 0);
        }

        [Test]
        public void GetAssetDetails_ThrowsForInvalidAssetId()
        {
            var testAssetGuidId = Guid.NewGuid();
            var testYear = 2023;

            var expected = new AssetDetailsViewModel
            {
                AssetId = testAssetGuidId,
                Name = String.Empty,
                BeneficiaryName = "beneficiary name",
            };

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await assetService
                    .GetAssetDetailsAsync(testUserId, testAssetGuidId, testYear));
        }

        [Test]
        public void GetAssetDetails_ThrowsForInvalidUser()
        {
            var testAssetGuidId = context.Assets.First().Id;
            string invalidTestUserId = "InvalidUserId";
            var testYear = 2023;

            var expected = new AssetDetailsViewModel
            {
                AssetId = testAssetGuidId,
                Name = String.Empty,
                BeneficiaryName = "beneficiary name",
            };

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await assetService
                    .GetAssetDetailsAsync(invalidTestUserId, testAssetGuidId, testYear));
        }

        [Test]
        public async Task GetAssetDetails_SetsReportValueToZeroIfTargetYearIsEarlierThatYearOfAquisition()
        {
            var testAssetGuidId = context.Assets.First().Id;
            var testYear = 2000;

            decimal expectedReportValue = 0;
            var result = await assetService
                .GetAssetDetailsAsync(testUserId, testAssetGuidId, testYear);

            Assert.That(result.AssetMonthlyStatus.All(a => a.ReportValue == expectedReportValue));
        }
    }
}
