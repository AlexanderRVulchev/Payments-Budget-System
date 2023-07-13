using Microsoft.EntityFrameworkCore;
using PaymentsBudgetSystem.Core.Contracts;
using PaymentsBudgetSystem.Core.Models.Report;
using PaymentsBudgetSystem.Core.Services;
using PaymentsBudgetSystem.Data;
using PaymentsBudgetSystem.Data.Entities;
using PaymentsBudgetSystem.Data.Entities.Enums;

namespace PaymentsBudgetSystem.Tests.Services
{
    internal class ReportServiceTests
    {
        private PBSystemDbContext context;

        private IReportService reportService;

        private string primaryTestUserId;
        private string secondaryTestUserId;
        private int testYear;
        private int testMonth;

        [SetUp]
        public async Task Setup()
        {
            primaryTestUserId = "primary test user id";
            secondaryTestUserId = "secondary test user id";
            testYear = 2023;
            testMonth = 1;

            var individualBudgets = new List<IndividualBudget>()
            {
                new IndividualBudget
                {
                    AssetsLimit = 100000,
                    SalariesLimit = 100000,
                    SupportLimit = 100000,
                    UserId = primaryTestUserId,
                    FiscalYear = testYear
                },
                new IndividualBudget
                {
                    AssetsLimit = 100000,
                    SalariesLimit = 100000,
                    SupportLimit = 100000,
                    UserId = secondaryTestUserId,
                    FiscalYear = testYear
                }
            };

            var consolidatedBudget = new ConsolidatedBudget
            {
                FiscalYear = testYear,
                TotalLimit = 500000,
                UserId = primaryTestUserId
            };

            var userDependancy = new UserDependancy
            {
                PrimaryUserId = primaryTestUserId,
                SecondaryUserId = secondaryTestUserId
            };

            var reports = new List<Report>()
            {
                new Report
                {
                    Month = testMonth,
                    Year = testYear,
                    IsConsolidated = false,
                    UserId = secondaryTestUserId,
                    LimitAssets = 100000,
                    LimitSalaries = 100000,
                    LimitSupport = 100000,
                    Bank1015 = 5
                },
                new Report
                {
                    Month = testMonth,
                    Year = testYear,
                    IsConsolidated = false,
                    UserId = primaryTestUserId
                },
                new Report
                {
                    Month = testMonth,
                    Year = testYear,
                    IsConsolidated = true,
                    UserId = primaryTestUserId
                }
            };

            var payments = new List<Payment>()
            {
                new Payment
                {
                    Amount = 10,
                    PaymentType = PaymentType.Support,
                    Paragraph = ParagraphType.Materials1015,
                    UserId = primaryTestUserId,
                    Date = new DateTime(2023, 1, 1),
                    ReceiverName = ""
                },
                new Payment
                {
                    Amount = 5,
                    PaymentType = PaymentType.Support,
                    Paragraph = ParagraphType.Materials1015,
                    UserId = secondaryTestUserId,
                    Date = new DateTime(2023, 1, 1),
                    ReceiverName = ""
                }
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                   .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                   .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();

            await context.UsersDependancies.AddAsync(userDependancy);
            await context.IndividualBudgets.AddRangeAsync(individualBudgets);
            await context.ConsolidatedBudgets.AddAsync(consolidatedBudget);
            await context.Reports.AddRangeAsync(reports);
            await context.Payments.AddRangeAsync(payments);

            await context.SaveChangesAsync();

            reportService = new ReportService(context);
        }

        [Test]
        public async Task AddReportAnnotationsAsync_AddsAppropriateValuesToTheAssignedModelWhenReportIsConsolidated()
        {
            var testModel = new ReportInquiryViewModel
            {
                InstitutionId = primaryTestUserId,
                IsConsolidated = true,
                Month = testMonth,
                Year = testYear
            };

            await reportService
                .AddReportAnnotationsAsync(primaryTestUserId, testModel);

            Assert.That(testModel.IndividualReports.Count, Is.EqualTo(1));
            Assert.That(testModel.ConsolidatedReports.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddReportAnnotationsAsync_AddsAppropriateValuesToTheAssignedModelWhenReportIsIndividual()
        {
            var testModel = new ReportInquiryViewModel
            {
                InstitutionId = primaryTestUserId,
                IsConsolidated = false,
                Month = testMonth,
                Year = testYear
            };

            await reportService
                .AddReportAnnotationsAsync(primaryTestUserId, testModel);

            Assert.That(testModel.IndividualReports.Count, Is.EqualTo(1));
            Assert.That(testModel.ConsolidatedReports.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task BuildConsolidatedReportAsync_ReturnsCorrectModel()
        {
            decimal expectedSupportLimit = 200000;
            decimal expectedSalariesLimit = 200000;
            decimal expectedAssetsLimit = 200000;
            decimal expentedExpenses = 15;

            var result = await reportService
                .BuildConsolidatedReportAsync(primaryTestUserId, testYear, testMonth);

            Assert.IsNotNull(result);
            Assert.That(result.AssetsLimit, Is.EqualTo(expectedAssetsLimit));
            Assert.That(result.SupportLimit, Is.EqualTo(expectedSupportLimit));
            Assert.That(result.SalariesLimit, Is.EqualTo(expectedSalariesLimit));
            Assert.That(result.Bank1015, Is.EqualTo(expentedExpenses));
        }

        [Test]
        public async Task BuildIndividualReportAsync_ReturnsCorrectModel()
        {
            decimal expectedSupportLimit = 100000;
            decimal expectedSalariesLimit = 100000;
            decimal expectedAssetsLimit = 100000;
            decimal expentedExpenses = 10;

            var result = await reportService
                .BuildIndividualReportAsync(primaryTestUserId, testYear, testMonth);

            Assert.IsNotNull(result);
            Assert.That(result.AssetsLimit, Is.EqualTo(expectedAssetsLimit));
            Assert.That(result.SupportLimit, Is.EqualTo(expectedSupportLimit));
            Assert.That(result.SalariesLimit, Is.EqualTo(expectedSalariesLimit));
            Assert.That(result.Bank1015, Is.EqualTo(expentedExpenses));
        }

        [Test]
        public void BuildIndividualReportAsync_ThrowsForMissingBudget()
        {
            var invalidReportYear = 2022;

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await reportService
                    .BuildIndividualReportAsync(primaryTestUserId, invalidReportYear, testMonth));
        }

        [Test]
        public async Task GetReportByIdAsync_ReturnsCorrectModel()
        {
            var report = await context.Reports
                .Where(r => r.UserId == secondaryTestUserId)
                .FirstAsync();

            var reportId = report.Id;
            decimal expectedExpenses = 5;
            decimal expectedSupportLimit = 100000;
            decimal expectedSalariesLimit = 100000;
            decimal expectedAssetLimit = 100000;

            var result = await reportService.GetReportByIdAsync(reportId);

            Assert.IsNotNull(result);
            Assert.That(result.AssetsLimit, Is.EqualTo(expectedAssetLimit));
            Assert.That(result.SupportLimit, Is.EqualTo(expectedSupportLimit));
            Assert.That(result.SalariesLimit, Is.EqualTo(expectedSalariesLimit));
            Assert.That(result.Bank1015, Is.EqualTo(expectedExpenses));
        }

        [Test]
        public void GetReportByIdAsync_ThrowsForMissingReport()
        {
            var invalidReportId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await reportService
                    .GetReportByIdAsync(invalidReportId));
        }

        [Test]
        public async Task SaveIndividualReportAsync_CreatesAppropriateEntity()
        {
            var testMonthForNewReport = 2;
            var testSalariesLimit = 50000;
            var testBank1020 = 100;

            var testModel = new ReportDataModel
            {
                Month = testMonthForNewReport,
                Year = testYear,
                SalariesLimit = testSalariesLimit,
                Bank1020 = testBank1020,
            };

            await reportService
                .SaveIndividualReportAsync(primaryTestUserId, testModel);

            var createdEntity = await context.Reports
                .Where(r => r.Month == testMonthForNewReport && r.Year == testYear)
                .FirstAsync();

            Assert.That(createdEntity, Is.Not.Null);
            Assert.That(createdEntity.LimitSalaries == testSalariesLimit);
            Assert.That(createdEntity.Bank1020 == testBank1020);
        }

        [Test]
        public async Task SaveIndividualReportAsync_TakesNoActionIfReportAlreadyExists()
        {
            int reportsCount = await context.Reports
                .CountAsync();

            var testModel = new ReportDataModel
            {
                Month = testMonth,
                Year = testYear
            };

            await reportService
                .SaveIndividualReportAsync(primaryTestUserId, testModel);

            Assert.That(await context.Reports.CountAsync(), Is.EqualTo(reportsCount));
        }
    }
}
