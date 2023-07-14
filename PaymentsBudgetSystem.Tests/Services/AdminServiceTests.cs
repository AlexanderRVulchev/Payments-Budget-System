using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Tests.Services
{
    using Core.Contracts;
    using Core.Models.Administration;
    using Core.Services;
    using Data;
    using Data.Entities;

    using static Tests.GlobalSettingsTestSeeder;
    using GlobalSetting = PaymentsBudgetSystem.Data.Entities.GlobalSetting;

    [TestFixture]
    internal class AdminServiceTests
    {
        private PBSystemDbContext context;

        private IAdminService adminService;

        private IReportService reportService;

        private string testUserId;

        [SetUp]
        public async Task Setup()
        {
            testUserId = "test user id";

            var globalSettings = GetGlobalSettings();

            var report = new Report
            {
                Month = 1,
                Year = 2023,
                UserId = testUserId
            };

            var user = new User
            {
                Id = testUserId,
                Name = "",
                UserName = ""
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();

            await context.Users.AddAsync(user);
            await context.GlobalSettings.AddRangeAsync(globalSettings);
            await context.Reports.AddAsync(report);

            await context.SaveChangesAsync();

            reportService = new ReportService(context);
            adminService = new AdminService(context, reportService);
        }

        [Test]
        public async Task DeleteReportByIdAsync_RemovesTheReportFromTheDatabase()
        {
            var reportId = await context.Reports
                .Select(r => r.Id)
                .FirstAsync();

            await adminService.DeleteReportByIdAsync(reportId);

            Assert.IsFalse(await context.Reports.AnyAsync());
        }

        [Test]
        public void DeleteReportByIdAsync_ThrowsForInvalidReportId()
        {
            var invalidReportId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await adminService.DeleteReportByIdAsync(invalidReportId));
        }

        [Test]
        public async Task GetAllReportsAsync_ReturnsCorrectModel()
        {
            var result = await adminService.GetAllReportsAsync();

            Assert.IsNotNull(result);
            Assert.That(result.InstitutionsWithReports.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetGlobalSettingsAsync_ReturnsCorrectModel()
        {
            var globalSettings = GetGlobalSettings();

            var result = await adminService.GetGlobalSettingsAsync();

            Assert.That(result.IntangibleAssetLife, Is.EqualTo(globalSettings[(int)Core.Models.Enums.GlobalSetting.IntangibleAssetLife - 1].SettingValue));
            Assert.That(result.TaxRate, Is.EqualTo(globalSettings[(int)Core.Models.Enums.GlobalSetting.TaxRate - 1].SettingValue * 100));
            Assert.That(result.UpkeepLife, Is.EqualTo(globalSettings[(int)Core.Models.Enums.GlobalSetting.UpkeepLife - 1].SettingValue));
            Assert.That(result.AdditionalInsuranceEmployeePercentage, Is.EqualTo(globalSettings[(int)Core.Models.Enums.GlobalSetting.AdditionalInsuranceEmployee - 1].SettingValue * 100));
            Assert.That(result.MinimumWage, Is.EqualTo(globalSettings[(int)Core.Models.Enums.GlobalSetting.MinimumWage - 1].SettingValue));
        }

        [Test]
        public async Task SaveGlobalSettingsAsync_ProperlySavesTheSettings()
        {
            var testModel = new GlobalSettingsEditModel
            {
                MinimumWage = 100,
                HealthInsuranceEmployerPercentage = 10,
                IntangibleAssetLife = 5,
                TaxRate = 15
            };

            await adminService
                .SaveGlobalSettingsAsync(testModel);

            var newSettings = await context.GlobalSettings
                .ToListAsync();

            Assert.That(newSettings[(int)Core.Models.Enums.GlobalSetting.IntangibleAssetLife - 1].SettingValue, Is.EqualTo(testModel.IntangibleAssetLife));
            Assert.That(newSettings[(int)Core.Models.Enums.GlobalSetting.HealthInsuranceEmployer - 1].SettingValue * 100, Is.EqualTo(testModel.HealthInsuranceEmployerPercentage));
            Assert.That(newSettings[(int)Core.Models.Enums.GlobalSetting.MinimumWage - 1].SettingValue, Is.EqualTo(testModel.MinimumWage));
            Assert.That(newSettings[(int)Core.Models.Enums.GlobalSetting.TaxRate - 1].SettingValue * 100, Is.EqualTo(testModel.TaxRate));
        }
    }
}
