using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Tests.Services
{
    using Core.Contracts;
    using Core.Models.Assets;
    using Core.Models.Cash;
    using Core.Models.Salaries;
    using Core.Models.Support;
    using Core.Services;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;

    using static Tests.GlobalSettingsTestSeeder;

    [TestFixture]
    internal class PaymentServiceTests
    {
        private PBSystemDbContext context;
        private IPaymentService paymentService;
        private IEmployeeService employeeService;
        private IBeneficiaryService beneficiaryService;
        private IReportService reportService;

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
                        Assets = new HashSet<Asset>()
                        {
                            new Asset
                            {
                                DateAquired = new DateTime(2023, 1, 1),
                                Description = "",
                                ReportValue = 10,
                                UserId = testUserId                                
                            }
                        }
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
                            Employee = employee,                            
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

            var individualBudget = new IndividualBudget
            {
                UserId = testUserId,
                FiscalYear = 2023,
                AssetsLimit = 100000,
                SalariesLimit = 100000,
                SupportLimit = 100000,
            };

            var globalSettings = GetGlobalSettings();

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                 .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                 .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Beneficiaries.AddAsync(beneficiary);
            await context.Employees.AddAsync(employee);
            await context.Payments.AddRangeAsync(payments);
            await context.IndividualBudgets.AddAsync(individualBudget);
            await context.GlobalSettings.AddRangeAsync(globalSettings);

            await context.SaveChangesAsync();

            beneficiaryService = new BeneficiaryService(context);
            employeeService = new EmployeeService(context);
            reportService = new ReportService(context);
            paymentService = new PaymentService(context, beneficiaryService, employeeService, reportService);
        }

        [Test]
        public async Task AddNewAssetPayment_CreatesEntitiesCorrectly()
        {
            string testAssetName = "new asset name";
            decimal testAssetCost = 10;
            decimal totalPaymentValue = 50;

            var testModel = new NewAssetFormModel
            {
                BeneficiaryId = await context.Beneficiaries.Select(b => b.Id).FirstAsync(),
                InvoiceDate = new DateTime(2023, 1, 1),
                InvoiceNumber = "",
                ParagraphType = ParagraphType.UpkeepLongTermAssets5100,
                Position1Name = testAssetName,
                Position1SingleAssetValue = testAssetCost,
                Position1Quantity = 1,
                Position2Name = testAssetName,
                Position2SingleAssetValue = testAssetCost,
                Position2Quantity = 1,
                Position3Name = testAssetName,
                Position3SingleAssetValue = testAssetCost,
                Position3Quantity = 1,
                Position4Name = testAssetName,
                Position4SingleAssetValue = testAssetCost,
                Position4Quantity = 1,
                Position5Name = testAssetName,
                Position5SingleAssetValue = testAssetCost,
                Position5Quantity = 1,
            };

            var resultAssetPaymentId = await paymentService
                .AddNewAssetPaymentAsync(testUserId, testModel);

            var actualAssetEntity = await context.Assets
                .Where(a => a.PaymentAssetsDetails.AssetPaymentId == resultAssetPaymentId)
                .FirstAsync();

            var actualPaymentEntity = await context.Payments
                .FindAsync(resultAssetPaymentId);

            Assert.That(actualAssetEntity, Is.Not.Null);
            Assert.That(actualAssetEntity.Description, Is.EqualTo(testAssetName));
            Assert.That(actualAssetEntity.ReportValue, Is.EqualTo(testAssetCost));
            Assert.That(actualPaymentEntity!.Amount, Is.EqualTo(totalPaymentValue));
        }

        [Test]
        public async Task AddNewAssetPayment_ThrowsForExpenseHigherThanTheAnnualLimit()
        {
            string testAssetName = "new asset name";
            decimal testPaymentValue = 1000000;

            var testModel = new NewAssetFormModel
            {
                BeneficiaryId = await context.Beneficiaries.Select(b => b.Id).FirstAsync(),
                InvoiceDate = new DateTime(2023, 1, 1),
                InvoiceNumber = "",
                ParagraphType = ParagraphType.UpkeepLongTermAssets5100,
                Position1Name = testAssetName,
                Position1SingleAssetValue = testPaymentValue,
                Position1Quantity = 1,
            };

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await paymentService.AddNewAssetPaymentAsync(testUserId, testModel));
        }

        [Test]
        public async Task AddNewCashPayment_CreatesEntityCorrectly()
        {
            decimal testPaymentValue = 10;

            var testModel = new CashPaymentViewModel
            {
                Amount = testPaymentValue,
                CashOrderNumber = 1,
                SelectedEmployee = await context.Employees.Select(e => e.Id).FirstAsync(),
                Date = new DateOnly(2023, 1, 1),
                Type = ParagraphType.Materials1015
            };

            var resultPaymentId = await paymentService.AddNewCashPaymentAsync(testUserId, testModel);

            var actualEntity = await context.CashPaymentDetails.FindAsync(resultPaymentId);

            Assert.IsNotNull(actualEntity);
            Assert.That(actualEntity.Payment.Amount, Is.EqualTo(testPaymentValue));
        }

        [Test]
        public async Task AddNewCashPayment_ThrowsForExpenseHigherThanTheAnnualLimit()
        {
            decimal testPaymentValue = 1000000;

            var testModel = new CashPaymentViewModel
            {
                Amount = testPaymentValue,
                CashOrderNumber = 1,
                SelectedEmployee = await context.Employees.Select(e => e.Id).FirstAsync(),
                Date = new DateOnly(2023, 1, 1),
            };

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await paymentService.AddNewCashPaymentAsync(testUserId, testModel));
        }

        [Test]
        public async Task AddNewSalariesPayment_CreatesEntitiesCorrectly()
        {
            int testMonth = 1;
            int testYear = 2023;

            var testModel = new SalariesPaymentViewModel
            {
                Amount = 10,
                Year = testYear,
                Month = testMonth,
                IndividualSalaries = new List<EmployeeSalaryPaymentViewModel>()
                {
                    new EmployeeSalaryPaymentViewModel
                    {
                        EmployeeName = ""                        
                    }
                }
            };

            var resultPaymentId = await paymentService
                .AddNewSalariesPaymentAsync(testUserId, testModel);

            var actualEntity = await context.Payments.FindAsync(resultPaymentId);

            Assert.IsNotNull(actualEntity);
            Assert.That(actualEntity.SalariesDetails.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddNewSalariesPayment_ThrowsForExpenseHigherThanTheAnnualLimit()
        {
            decimal testPaymentValue = 1000000;

            var testModel = new SalariesPaymentViewModel
            {
                Amount = testPaymentValue,
            };

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await paymentService
                    .AddNewSalariesPaymentAsync(testUserId, testModel));
        }

        [Test]
        public async Task AddNewSupportPayment_CreatesEntityCorrectly()
        {
            decimal testPaymentValue = 10;
            Guid testBeneficiaryId = await context.Beneficiaries
                .Select(b => b.Id)
                .FirstAsync();

            var testModel = new SupportPaymentFormModel
            {
                Amount = testPaymentValue,
                BeneficiaryId = testBeneficiaryId,
                InvoiceNumber = ""
            };

            var resultPaymentId = await paymentService
                .AddNewSupportPaymentAsync(testUserId, testModel);

            var actualEntity = await context.PaymentSupportDetails
                .FindAsync(resultPaymentId);

            Assert.IsNotNull(actualEntity);
            Assert.That(actualEntity.BeneficiaryId, Is.EqualTo(testBeneficiaryId));
        }

        [Test]
        public void AddNewSupportPayment_ThrowsForExpenseHigherThanTheAnnualLimit()
        {
            decimal testPaymentValue = 1000000;

            var testModel = new SupportPaymentFormModel
            {
                Amount = testPaymentValue,
            };

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await paymentService
                    .AddNewSupportPaymentAsync(testUserId, testModel));
        }

        [Test]
        public async Task CreatePayroll_ReturnsCorrectModel()
        {
            int testYear = 2023;
            int testMonth = 1;

            decimal expectedTotalSalariesAmount = 4351.20m;

            var result = await paymentService.CreatePayrollAsync(testUserId, testYear, testMonth);

            Assert.IsNotNull(result);
            Assert.That(result.Amount, Is.EqualTo(expectedTotalSalariesAmount));
        }

        [Test]
        public async Task GetAssetPaymentDetailsById_ReturnsCorrectModel()
        {
            var actualEntity = await context.PaymentAssetsDetails
                .Include(pad => pad.Payment)
                .FirstAsync();

            var actualPaymentId = actualEntity.AssetPaymentId;

            var result = await paymentService.GetAssetPaymentDetailsByIdAsync(testUserId, actualPaymentId);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(actualPaymentId));
            Assert.That(result.InvoiceDate, Is.EqualTo(actualEntity.InvoiceDate));
            Assert.That(result.InvoiceNumber, Is.EqualTo(actualEntity.InvoiceNumber));
            Assert.That(result.Amount, Is.EqualTo(actualEntity.Payment.Amount));
            Assert.That(result.Beneficiary.BeneficiaryId, Is.EqualTo(actualEntity.BeneficiaryId));
        }

        [Test]
        public void GetAssetPaymentDetailsById_ThrowsForInvalidPaymentId()
        {
            var invalidPaymentId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService.GetAssetPaymentDetailsByIdAsync(testUserId, invalidPaymentId));
        }

        [Test]
        public async Task GetAssetPaymentDetailsById_ThrowsForInvalidUserId()
        {
            var invalidUserId = "invalid user id";
            var validPaymentId = await context.PaymentAssetsDetails.Select(pad => pad.AssetPaymentId).FirstAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService.GetAssetPaymentDetailsByIdAsync(invalidUserId, validPaymentId));
        }

        [Test]
        public async Task GetCashPaymentById_ReturnsCorrectModel()
        {
            var actualEntity = await context.CashPaymentDetails
                .Include(pad => pad.Payment)
                .FirstAsync();

            var actualPaymentId = actualEntity.CashPaymentId;

            var result = await paymentService.GetCashPaymentByIdAsync(testUserId, actualPaymentId);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(actualPaymentId));
            Assert.That(result.Date, Is.EqualTo(actualEntity.CashOrderDate));
            Assert.That(result.CashOrderNumber, Is.EqualTo(actualEntity.CashOrderNumber));
            Assert.That(result.Amount, Is.EqualTo(actualEntity.Payment.Amount));
            Assert.That(result.Employee.EmployeeId, Is.EqualTo(actualEntity.EmployeeId));
        }

        [Test]
        public void GetCashPaymentById_ThrowsForInvalidPaymentId()
        {
            var invalidPaymentId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService
                    .GetCashPaymentByIdAsync(testUserId, invalidPaymentId));
        }

        [Test]
        public async Task GetCashPaymentById_ThrowsForInvalidUserId()
        {
            var invalidUserId = "invalid user id";
            var validPaymentId = await context.PaymentAssetsDetails
                .Select(pad => pad.AssetPaymentId).FirstAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService.GetCashPaymentByIdAsync(invalidUserId, validPaymentId));
        }

        [Test]
        public async Task GetSalariesDetailsById_ReturnsCorrectModel()
        {
            var actualEntity = await context.PaymentSalariesDetails
                .Include(pad => pad.Payment)
                .FirstAsync();

            var actualPaymentId = actualEntity.Payment.Id;

            var result = await paymentService.GetSalariesDetailsByIdAsync(testUserId, actualPaymentId);

            Assert.IsNotNull(result);
            Assert.That(result.IndividualSalaries.Count, Is.EqualTo(1));
            Assert.That(result.Amount, Is.EqualTo(3));
        }

        [Test]
        public void GetSalariesDetailsById_ThrowsForInvalidPaymentId()
        {
            var invalidPaymentId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService
                    .GetSalariesDetailsByIdAsync(testUserId, invalidPaymentId));
        }

        [Test]
        public async Task GetSalariesDetailsById_ThrowsForInvalidUserId()
        {
            var invalidUserId = "invalid user id";
            var validPaymentId = await context.PaymentSalariesDetails
                .Select(psd => psd.PaymentId)
                .FirstAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService.GetSalariesDetailsByIdAsync(invalidUserId, validPaymentId));
        }

        [Test]
        public async Task GetSupportPaymentDetailsById_ReturnsCorrectModel()
        {
            var actualEntity = await context.PaymentSupportDetails
                .Include(pad => pad.Payment)
                .FirstAsync();

            var actualPaymentId = actualEntity.Payment.Id;

            var result = await paymentService.GetSupportPaymentDetailsByIdAsync(testUserId, actualPaymentId);

            Assert.IsNotNull(result);
            Assert.That(result.Amount, Is.EqualTo(1));
            Assert.That(result.InvoiceNumber, Is.EqualTo(actualEntity.InvoiceNumber));
            Assert.That(result.InvoiceDate, Is.EqualTo(actualEntity.InvoiceDate));
            Assert.That(result.Beneficiary.BeneficiaryId, Is.EqualTo(actualEntity.BeneficiaryId));
        }

        [Test]
        public void GetSupportPaymentDetailsById_ThrowsForInvalidPaymentId()
        {
            var invalidPaymentId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService
                    .GetSupportPaymentDetailsByIdAsync(testUserId, invalidPaymentId));
        }

        [Test]
        public async Task GetSupportPaymentDetailsById_ThrowsForInvalidUserId()
        {
            var invalidUserId = "invalid user id";
            var validPaymentId = await context.PaymentSupportDetails
                .Select(psd => psd.SupportPaymentId).FirstAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await paymentService.GetSupportPaymentDetailsByIdAsync(invalidUserId, validPaymentId));
        }
    }
}
