using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaymentsBudgetSystem.Core.Contracts;
using PaymentsBudgetSystem.Core.Models.Budget;
using PaymentsBudgetSystem.Core.Services;
using PaymentsBudgetSystem.Data;
using PaymentsBudgetSystem.Data.Entities;

namespace PaymentsBudgetSystem.Tests.Services
{
    [TestFixture]
    internal class BudgetServiceTests
    {
        private PBSystemDbContext context;
        private IReportService reportService;
        private IBudgetService budgetService;

        private string primaryTestUserId;
        private string secondaryTestUserId;

        [SetUp]
        public async Task Setup()
        {
            primaryTestUserId = "primary user test id";
            secondaryTestUserId = "secondary user test id";

            var users = new List<User>()
                {
                new User
                {
                    Id = primaryTestUserId,
                    Name = "",
                    UserName = ""
                },
                new User
                {
                    Id = secondaryTestUserId,
                    Name = "",
                    UserName = ""
                }
            };

            var userDependancy = new UserDependancy
            {
                PrimaryUserId = primaryTestUserId,
                SecondaryUserId = secondaryTestUserId
            };

            var consolidatedBudget = new ConsolidatedBudget
            {
                Id = Guid.NewGuid(),
                TotalLimit = 100,
                FiscalYear = 2022,
                UserId = primaryTestUserId
            };

            var individualBudget = new IndividualBudget
            {
                Id = Guid.NewGuid(),
                UserId = primaryTestUserId,
                SalariesLimit = 50,
                SupportLimit = 30,
                AssetsLimit = 20,
                FiscalYear = 2022
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
             .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
             .Options;

            context = new PBSystemDbContext(options);
            await context.Database.EnsureDeletedAsync();

            await context.Users.AddRangeAsync(users);
            await context.UsersDependancies.AddAsync(userDependancy);
            await context.ConsolidatedBudgets.AddAsync(consolidatedBudget);
            await context.IndividualBudgets.AddAsync(individualBudget);
            await context.SaveChangesAsync();

            reportService = new ReportService(context);
            budgetService = new BudgetService(context, reportService);
        }

        [Test]
        public async Task AddConsolidatedBudget_CreatesNewEntitiesProperly()
        {
            int testNewBudgetYear = 2023;
            decimal testNewBudgetFunds = 100;

            await budgetService.AddConsolidatedBudgetAsync(primaryTestUserId, testNewBudgetYear, testNewBudgetFunds);

            Assert.That(await context.ConsolidatedBudgets.CountAsync(), Is.EqualTo(2));
            Assert.That(await context.IndividualBudgets.CountAsync(), Is.EqualTo(3));
            Assert.That(await context.ConsolidatedBudgets.AnyAsync(b => b.FiscalYear == testNewBudgetYear));
            Assert.That(await context.ConsolidatedBudgets.AnyAsync(b => b.TotalLimit == testNewBudgetFunds));
        }

        [Test]
        public void AddConsolidatedBudget_ThrowsForExistingConsolidatedBudget()
        {
            int invalidYear = 2022;
            decimal testBudgetFunds = 100;

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await budgetService.AddConsolidatedBudgetAsync(primaryTestUserId, invalidYear, testBudgetFunds));
        }

        [Test]
        public async Task GetFullConsolidatedBudgetForPrimary_ReturnsCorrectModel()
        {
            int testYear = 2022;

            var actualConsolidatedBudget = await context.ConsolidatedBudgets.FirstAsync(b => b.UserId == primaryTestUserId && b.FiscalYear == testYear);


            var resultModel = await budgetService.GetFullConsolidatedBudgetForPrimaryAsync(primaryTestUserId, testYear);

            Assert.That(resultModel.IndividualBudgetsData.Count(), Is.EqualTo(1));
            Assert.That(resultModel.ConsolidatedBudget.TotalLimit, Is.EqualTo(actualConsolidatedBudget.TotalLimit));
            Assert.That(resultModel.ConsolidatedBudget.UserId, Is.EqualTo(primaryTestUserId));
        }

        [Test]
        public void GetFullConsolidatedBudgetForPrimary_ThrowsForInvalidUser()
        {
            string invalidUserId = "invalid user id";
            int testYear = 2022;

            Assert.ThrowsAsync<ArgumentNullException>(async ()
                => await budgetService.GetFullConsolidatedBudgetForPrimaryAsync(invalidUserId, testYear));
        }

        [Test]
        public void GetFullConsolidatedBudgetForPrimary_ThrowsForInvalidYear()
        {
            int invalidYear = 2023;

            Assert.ThrowsAsync<ArgumentNullException>(async ()
                => await budgetService.GetFullConsolidatedBudgetForPrimaryAsync(primaryTestUserId, invalidYear));
        }

        [Test]
        public async Task GetConsolidatedBudgetsAsync_ReturnsCorrectCollection()
        {
            var actualEntity = await context.ConsolidatedBudgets.FirstAsync(b => b.UserId == primaryTestUserId);

            var result = await budgetService.GetConsolidatedBudgetsAsync(primaryTestUserId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().UserId, Is.EqualTo(primaryTestUserId));
            Assert.That(result.First().TotalLimit, Is.EqualTo(actualEntity.TotalLimit));
            Assert.That(result.First().FiscalYear, Is.EqualTo(actualEntity.FiscalYear));
        }

        [Test]
        public async Task GetIndividualBudgetsAsync_ReturnsCorrectCollection()
        {
            var actualEntity = await context.IndividualBudgets.FirstAsync(b => b.UserId == primaryTestUserId);

            var result = await budgetService.GetIndividualBudgetsAsync(primaryTestUserId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().SupportLimit, Is.EqualTo(actualEntity.SupportLimit));
            Assert.That(result.First().AssetsLimit, Is.EqualTo(actualEntity.AssetsLimit));
            Assert.That(result.First().SalariesLimit, Is.EqualTo(actualEntity.SalariesLimit));
            Assert.That(result.First().FiscalYear, Is.EqualTo(actualEntity.FiscalYear));
        }

        [Test]
        public async Task EditBudget_ChangesTheEntityProperly()
        {
            var individualReportId = await context.IndividualBudgets.Select(b => b.Id).FirstAsync();
            decimal newSupportLimit = 11;
            decimal newAssetsLimit = 12;
            decimal newSalaryLimit = 13;
            int testYear = 2022;

            var testModel = new EditBudgetFormModel
            {
                Id = individualReportId,
                FiscalYear = testYear,
                NewSalaryLimit = newSalaryLimit,
                NewSupportLimit = newSupportLimit,
                NewAssetsLimit = newAssetsLimit,
            };

            await budgetService.EditBudgetAsync(testModel);

            var actualEntity = await context.IndividualBudgets.FindAsync(individualReportId);

            Assert.That(actualEntity, Is.Not.Null);
            Assert.That(actualEntity.SupportLimit, Is.EqualTo(newSupportLimit));
            Assert.That(actualEntity.SalariesLimit, Is.EqualTo(newSalaryLimit));
            Assert.That(actualEntity.AssetsLimit, Is.EqualTo(newAssetsLimit));            
        }

        [Test]
        public async Task EditBudget_ThrowsForInvalidBudgetId()
        {
            Guid invalidBudgetId = Guid.NewGuid();
            int testYear = 2022;

            var testModel = new EditBudgetFormModel
            {
                Id = invalidBudgetId,
                FiscalYear = testYear
            };

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await budgetService.EditBudgetAsync(testModel));
        }

        [Test]
        public async Task CreateBlankBudgetsForSecondaryUser()
        {
            var result = budgetService.CreateBlankBudgetsForSecondaryUserAsync(primaryTestUserId, secondaryTestUserId);

            Assert.That(await context.IndividualBudgets.CountAsync(), Is.EqualTo(2));
            Assert.That(await context.IndividualBudgets.AnyAsync(b => b.UserId == secondaryTestUserId));
        }
    }
}
