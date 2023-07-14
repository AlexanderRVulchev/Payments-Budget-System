using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;


namespace PaymentsBudgetSystem.Tests.Services
{
    using Core.Contracts;
    using Core.Services;
    using Data;
    using Data.Entities;

    using static Common.RoleNames;

    [TestFixture]
    internal class UserServiceTests
    {
        private PBSystemDbContext context;
        private IUserService userService;
        private IBudgetService budgetService;
        private IReportService reportService;

        private string primaryTestUserId = "primary test user id";
        private string secondaryTestUserId = "secondary test user id";
        private string primaryTestUserName = "primary test user name";
        private string secondaryTestUserName = "secondary test user name";

        [SetUp]
        public async Task Setup()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = primaryTestUserId,
                    IsPrimary = true,
                    UserName = "primary test username",
                    Name = primaryTestUserName
                },
                new User
                {
                    Id = secondaryTestUserId,
                    IsPrimary = false,
                    UserName = "secondary test username",
                    Name = secondaryTestUserName,
                }
            };

            var report = new Report
            {
                IsConsolidated = false,
                UserId = primaryTestUserId
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
              .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
              .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Users.AddRangeAsync(users);
            await context.Reports.AddAsync(report);
            await context.SaveChangesAsync();

            var userManagerMock = new Mock<UserManager<User>>(
                    Mock.Of<IUserStore<User>>(),
                    Mock.Of<IOptions<IdentityOptions>>(),
                    Mock.Of<IPasswordHasher<User>>(),
                    It.IsAny<IEnumerable<IUserValidator<User>>>(),
                    It.IsAny<IEnumerable<IPasswordValidator<User>>>(),
                    Mock.Of<ILookupNormalizer>(),
                    Mock.Of<IdentityErrorDescriber>(),
                    Mock.Of<IServiceProvider>(),
                    Mock.Of<ILogger<UserManager<User>>>());

            userManagerMock.Setup(um => um.IsInRoleAsync(users[0], PrimaryRoleName)).ReturnsAsync(true);

            reportService = new ReportService(context);
            budgetService = new BudgetService(context, reportService);
            userService = new UserService(context, userManagerMock.Object, budgetService);
        }

        [Test]
        public async Task GetPrimaryNamesAsync_ReturnsNamesOfPrimaryUsers()
        {
            var result = await userService.GetPrimaryNamesAsync();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(primaryTestUserName));
        }

        [Test]
        public async Task GetPrimaryIdsAndNamesAsync_ReturnsTheExpectedDictionary()
        {
            var result = await userService.GetPrimaryIdsAndNamesAsync();

            Assert.That(result.First().Key, Is.EqualTo(primaryTestUserId));
            Assert.That(result.First().Value, Is.EqualTo(primaryTestUserName));
        }

        [Test]
        public async Task RelateSecondaryToPrimaryUserAsync_CreatesProperUserDependancyObject()
        {
            await userService.RelateSecondaryToPrimaryUserAsync(primaryTestUserId, secondaryTestUserId);

            var createdUserDependancyEntity = await context.UsersDependancies.FirstOrDefaultAsync();

            Assert.IsNotNull(createdUserDependancyEntity);
            Assert.That(createdUserDependancyEntity.PrimaryUserId, Is.EqualTo(primaryTestUserId));
            Assert.That(createdUserDependancyEntity.SecondaryUserId, Is.EqualTo(secondaryTestUserId));
        }

        [Test]
        public async Task GetAllUsersWithSavedReportsAsync_ReturnsCorrectModelCollection()
        {
            var result = await userService.GetAllUsersWithSavedReportsAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

    }
}
