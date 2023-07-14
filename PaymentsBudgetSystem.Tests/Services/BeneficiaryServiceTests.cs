using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Tests.Services
{
    using Core.Contracts;
    using Core.Models.Beneficiaries;
    using Core.Services;
    using Data;
    using Data.Entities;

    [TestFixture]
    internal class BeneficiaryServiceTests
    {
        private PBSystemDbContext context;
        private IBeneficiaryService beneficiaryService;

        private string testUserId;

        [SetUp]
        public async Task Setup()
        {
            testUserId = "test user id";

            var beneficiaries = new List<Beneficiary>
            {
                new Beneficiary
                {
                    Id = Guid.NewGuid(),
                    Address = "test address 1",
                    BankAccount = "00AAAA000000000000",
                    Identifier = "000000000",
                    Name = "test name 1",
                    UserId = testUserId,                    
                },
                new Beneficiary
                {
                    Id = Guid.NewGuid(),
                    Address = "test address 2",
                    BankAccount = "00AAAA000000000001",
                    Identifier = "000000001",
                    Name = "test name 2",
                    UserId = testUserId
                }
            };

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                UserId = testUserId,
                ReceiverName = beneficiaries.First().Name                
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();

            await context.Beneficiaries.AddRangeAsync(beneficiaries);
            await context.Payments.AddAsync(payment);

            await context.SaveChangesAsync();

            beneficiaryService = new BeneficiaryService(context);
        }

        [Test]
        public async Task AddBeneficiary_AddsEntityCorrectly()
        {
            var testModel = new BeneficiaryFormModel
            {
                Id = Guid.NewGuid(),
                Address = "test address",
                Identifier = "999999999",
                BankAccount = "99AAAA999999999999",
                Name = "test name"
            };

            await beneficiaryService.AddBeneficiaryAsync(testUserId, testModel);

            var expectedEntity = await context.Beneficiaries
                .Where(b => b.Address == testModel.Address)
                .Where(b => b.BankAccount == testModel.BankAccount)
                .Where(b => b.Identifier == testModel.Identifier)
                .Where(b => b.Name == testModel.Name)
                .FirstOrDefaultAsync();

            Assert.NotNull(expectedEntity);
            Assert.That(await context.Beneficiaries.CountAsync() == 3);
        }

        [Test]
        public async Task AddBeneficiary_ThrowsForExistingBeneficiary()
        {
            var testModel = new BeneficiaryFormModel
            {
                Id = Guid.NewGuid(),
                Address = "test address",
                Identifier = context.Beneficiaries.First().Identifier,
                BankAccount = "99AAAA999999999999",
                Name = context.Beneficiaries.First().Name
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await beneficiaryService.AddBeneficiaryAsync(testUserId, testModel));
        }

        [Test]
        public async Task EditBeneficiary_ChangesTheEntityCorrectly()
        {
            Guid entityId = await context.Beneficiaries.Select(b => b.Id).FirstAsync();
            string newAddress = "new address";
            string newIdentifier = "777777777";
            string newBankAccount = "77AAAA777777777777";
            string newName = "new test name";

            var testModel = new BeneficiaryFormModel
            {
                Id = entityId,
                Address = newAddress,
                Identifier = newIdentifier,
                BankAccount = newBankAccount,
                Name = newName
            };

            await beneficiaryService
                .EditBeneficiaryAsync(testUserId, testModel);

            var expectedEntity = await context.Beneficiaries
                .Where(b => b.Id == entityId)
                .Where(b => b.Address == newAddress)
                .Where(b => b.Identifier == newIdentifier)
                .Where(b => b.Name == newName)
                .Where(b => b.BankAccount == newBankAccount)
                .Where(b => b.UserId == testUserId)
                .FirstOrDefaultAsync();

            var actualEntity = await context.Beneficiaries
                .FindAsync(entityId);

            Assert.IsNotNull(expectedEntity);
            Assert.That(await context.Payments.AnyAsync(p => p.ReceiverName == expectedEntity.Name));
        }

        [Test]
        public void EditBeneficiary_ThrowsForInvalidBeneficiaryId()
        {
            var testModel = new BeneficiaryFormModel
            {
                Id = Guid.NewGuid()
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () 
                => await beneficiaryService
                    .EditBeneficiaryAsync(testUserId, testModel));
        }

        [Test]
        public async Task EditBeneficiary_ThrowsForInvalidUser()
        {
            Guid entityId = await context.Beneficiaries.Select(b => b.Id).FirstAsync();
            string invalidUserId = "invalid user";

            var testModel = new BeneficiaryFormModel
            {
                Id = entityId                
            };

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await beneficiaryService
                    .EditBeneficiaryAsync(invalidUserId, testModel));
        }

        [Test]
        public async Task GetAllBeneficiaries_ReturnsCorrectModel()
        {
            var testModel = new AllBeneficiariesViewModel();            

            var result = await beneficiaryService
                .GetAllBeneficiariesAsync(testUserId, testModel);

            Assert.That(result.Beneficiaries.Count, Is.EqualTo(2));
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.NumberOfPages, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllBeneficiaries_SetsThePageCorrectly()
        {
            var testModel = new AllBeneficiariesViewModel
            {
                Page = 10,
                NumberOfPages = 1
            };

            var result = await beneficiaryService
                .GetAllBeneficiariesAsync(testUserId, testModel);

            Assert.That(result.Beneficiaries.Count, Is.EqualTo(2));
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.NumberOfPages, Is.EqualTo(1));
        }

        [Test]
        public async Task GetBeneficiary_ReturnCorrectModel()
        {
            var entity = await context.Beneficiaries
                .FirstAsync();

            var result = await beneficiaryService
                .GetBeneficiaryAsync(testUserId, entity.Id);

            Assert.That(result.Id, Is.EqualTo(entity.Id));
            Assert.That(result.Identifier, Is.EqualTo(entity.Identifier));
            Assert.That(result.Name, Is.EqualTo(entity.Name));
            Assert.That(result.BankAccount, Is.EqualTo(entity.BankAccount));
            Assert.That(result.Address, Is.EqualTo(entity.Address));
        }

        [Test]
        public void GetBeneficiary_ThrowsForInvalidBeneficiaryId()
        {
            var invalidBeneficiaryId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async () 
                => await beneficiaryService
                    .GetBeneficiaryAsync(testUserId, invalidBeneficiaryId));
        }

        [Test]
        public async Task GetBeneficiary_ThrowsForInvalidUser()
        {
            var beneficiaryId = await context.Beneficiaries.Select(b => b.Id)
                .FirstAsync();

            var invalidUserId = "invalid user id";

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await beneficiaryService
                    .GetBeneficiaryAsync(invalidUserId, beneficiaryId));
        }
    }
}
