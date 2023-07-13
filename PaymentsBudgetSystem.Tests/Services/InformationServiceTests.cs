
namespace PaymentsBudgetSystem.Tests.Services
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using PaymentsBudgetSystem.Core.Contracts;
    using PaymentsBudgetSystem.Core.Models.Information;
    using PaymentsBudgetSystem.Core.Services;
    using PaymentsBudgetSystem.Data.Entities;
    using PaymentsBudgetSystem.Data.Entities.Enums;

    [TestFixture]
    internal class InformationServiceTests
    {
        private PBSystemDbContext context;

        private IInformationService informationService;

        private string testUserId;

        [SetUp]
        public async Task Setup()
        {
            testUserId = "test user id";
            string beneficiaryTestName = "beneficiary test name";
            string employeeTestFirstName = "employee test first name";
            string employeeTestLastName = "employee test last name";

            var beneficiary = new Beneficiary
            {
                BankAccount = "test beneficiary bank account",
                Identifier = "test beneficiary identifier",
                Name = beneficiaryTestName,
                UserId = testUserId
            };

            var employee = new Employee
            {
                DateEmployed = new DateTime(2023, 1, 1),
                Egn = "test Egn",
                FirstName = employeeTestFirstName,
                LastName = employeeTestLastName,
                MonthlySalary = 3500,
                UserId = testUserId
            };

            var payments = new List<Payment>()
            {
                new Payment
                {
                    Amount = 1,
                    Date = new DateTime(2023, 1, 1),
                    Description = String.Empty,
                    ReceiverName = beneficiaryTestName,
                    UserId = testUserId,
                    SupportDetails = new PaymentSupportDetails
                    {
                        InvoiceDate = new DateTime(2023, 1, 1),
                        InvoiceNumber = "",
                        Beneficiary = beneficiary,
                    },
                    PaymentType = PaymentType.Support
                },
                new Payment
                {
                    Amount = 2,
                    Date = new DateTime(2023, 1, 1),
                    Description = String.Empty,
                    ReceiverName = beneficiaryTestName,
                    UserId = testUserId,
                    AssetsDetails = new PaymentAssetsDetails
                    {
                        InvoiceDate = new DateTime(2023, 1, 1),
                        InvoiceNumber = "",
                        Beneficiary = beneficiary,
                        DeliveryDate = new DateTime(2023, 1, 1),
                    },
                    PaymentType = PaymentType.Assets
                },
                new Payment
                {
                    Amount = 3,
                    Date = new DateTime(2023, 1, 1),
                    Description = String.Empty,
                    ReceiverName = employeeTestFirstName + " " + employeeTestLastName,
                    UserId = testUserId,
                    SalariesDetails = new List<PaymentSalaryDetails>()
                    {
                        new PaymentSalaryDetails
                        {
                            Employee = employee
                        }
                    },
                    PaymentType = PaymentType.Salaries
                },
                new Payment
                {
                    Amount = 4,
                    Date = new DateTime(2023, 1, 1),
                    Description = String.Empty,
                    ReceiverName = employeeTestFirstName + " " + employeeTestLastName,
                    UserId = testUserId,
                    CashDetails = new CashPaymentDetails
                    {
                        CashOrderDate  = new DateTime(2023, 1, 1),
                        CashOrderNumber = 1,
                        Employee = employee,
                    },
                    PaymentType = PaymentType.Cash
                }
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                   .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                   .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Beneficiaries.AddAsync(beneficiary);
            await context.Employees.AddAsync(employee);
            await context.Payments.AddRangeAsync(payments);

            await context.SaveChangesAsync();

            informationService = new InformationService(context);
        }

        [Test]
        public async Task GetPaymentsInfo_ReturnsProperModel()
        {
            var testModel = new PaymentInformationViewModel
            {
                NumberOfPages = 1,
                Page = 0,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 2, 2),
                AmountMin = 1,
                AmountMax = 4,
            };

            var result = await informationService.GetPaymentsInfoAsync(testUserId, testModel);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.NumberOfPages, Is.EqualTo(1));
            Assert.That(result.Payments, Has.Count.EqualTo(4));
        }

        [Test]
        public async Task GetPaymentInfo_ChagesThePageAccordingly()
        {
            var testModel = new PaymentInformationViewModel
            {
                NumberOfPages = 1,
                Page = 99,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 2, 2),
            };

            var result = await informationService.GetPaymentsInfoAsync(testUserId, testModel);

            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.NumberOfPages, Is.EqualTo(1));
        }
    }
}
