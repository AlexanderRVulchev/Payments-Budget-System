using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Tests.Helpers
{
    using Core.Helpers;
    using Core.Models.Assets;
    using Core.Models.Beneficiaries;
    using Core.Models.Employees;
    using Core.Models.Enums;
    using Core.Models.Information;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;

    [TestFixture]
    internal class SorterTests
    {
        private PBSystemDbContext context;
        private Sorter sorter;
        private List<AssetInfoViewModel> assetTestModel;

        [SetUp]
        public async Task Setup()
        {
            assetTestModel = new List<AssetInfoViewModel>
            {
                new AssetInfoViewModel
                {
                    Name = "A",
                    ReportValue = 0,
                    BalanceValue = 0,
                    AmortizationValue = 0,
                    DateAquired = new DateTime(2023, 1, 1)
                },
                new AssetInfoViewModel
                {
                    Name = "B",
                    ReportValue = 1,
                    BalanceValue = 1,
                    AmortizationValue = 1,
                    DateAquired = new DateTime(2023, 1, 2)
                },
                new AssetInfoViewModel
                {
                    Name = "C",
                    ReportValue = 2,
                    BalanceValue = 2,
                    AmortizationValue = 2,
                    DateAquired = new DateTime(2023, 1, 3)
                },
            };

            var beneficiaries = new List<Beneficiary>()
            {
                new Beneficiary
                {
                    Name = "0",
                    Address = "0",
                    BankAccount = "0",
                    Identifier = "0",
                    UserId = ""
                },
                new Beneficiary
                {
                    Name = "1",
                    Address = "1",
                    BankAccount = "1",
                    Identifier = "1",
                    UserId = ""
                },
                new Beneficiary
                {
                    Name = "2",
                    Address = "2",
                    BankAccount = "2",
                    Identifier = "2",
                    UserId = ""
                },
            };

            var employees = new List<Employee>()
            {
                new Employee
                {
                    DateEmployed = new DateTime(2023, 1, 1),
                    Egn = "0",
                    FirstName = "0",
                    LastName = "0",
                    MonthlySalary = 0,
                    UserId = ""
                },
                new Employee
                {
                    DateEmployed = new DateTime(2023, 1, 2),
                    Egn = "1",
                    FirstName = "1",
                    LastName = "1",
                    MonthlySalary = 1,
                    UserId = "1"
                },
                new Employee
                {
                    DateEmployed = new DateTime(2023, 1, 3),
                    Egn = "2",
                    FirstName = "2",
                    LastName = "2",
                    MonthlySalary = 2,
                    UserId = "2"
                }
            };

            var payments = new List<Payment>()
            {
                new Payment
                {
                    Amount = 1,
                    Date = new DateTime(2023, 1, 1),
                    Description = "A",
                    Paragraph = ParagraphType.LocalTransaction0000,
                    PaymentType = PaymentType.Cash,
                    ReceiverName = "A",
                    UserId = ""
                },
                new Payment
                {
                    Amount = 2,
                    Date = new DateTime(2023, 1, 2),
                    Description = "B",
                    Paragraph = ParagraphType.InsuranceAdditional0580,
                    PaymentType = PaymentType.Salaries,
                    ReceiverName = "B",
                    UserId = ""
                },
                new Payment
                {
                    Amount = 3,
                    Date = new DateTime(2023, 1, 3),
                    Description = "C",
                    Paragraph = ParagraphType.AquisitionIntangibleAssets5300,
                    PaymentType = PaymentType.Assets,
                    ReceiverName = "C",
                    UserId = ""
                },
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
                .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Beneficiaries.AddRangeAsync(beneficiaries);
            await context.Employees.AddRangeAsync(employees);
            await context.Payments.AddRangeAsync(payments);
            await context.SaveChangesAsync();

            sorter = new();
        }

        [Test]
        [TestCase(BeneficiarySort.Name)]
        [TestCase(BeneficiarySort.Identifier)]
        [TestCase(BeneficiarySort.Address)]
        [TestCase(BeneficiarySort.BankAccount)]
        [TestCase((BeneficiarySort)4)]
        public void SortBeneficiaries_ReturnsCorrectAssortedCollectionWhenSortingDescending(BeneficiarySort attributeSort)
        {
            IQueryable<Beneficiary> beneficiaries = context
                .Beneficiaries
                .AsQueryable();

            var testModel = new AllBeneficiariesViewModel
            {
                SortAttribute = attributeSort,
                SortBy = SortBy.Descending,
                BankAccountFilter = "",
                AddressFilter = "",
                IdentifierFilter = "",
                NameFilter = ""
            };

            var result = sorter.SortBeneficiaries(beneficiaries, testModel);

            if ((int)attributeSort < 4)
            {
                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result.First().Identifier, Is.EqualTo("2"));
                Assert.That(result.Last().Identifier, Is.EqualTo("0"));
                Assert.That(result.First().Name, Is.EqualTo("2"));
                Assert.That(result.Last().Name, Is.EqualTo("0"));
                Assert.That(result.First().BankAccount, Is.EqualTo("2"));
                Assert.That(result.Last().BankAccount, Is.EqualTo("0"));
                Assert.That(result.First().Address, Is.EqualTo("2"));
                Assert.That(result.Last().Address, Is.EqualTo("0"));
            }
            else
            {
                Assert.That(result.Count, Is.EqualTo(3));
            }
        }

        [Test]
        [TestCase(BeneficiarySort.Name)]
        [TestCase(BeneficiarySort.Identifier)]
        [TestCase(BeneficiarySort.Address)]
        [TestCase(BeneficiarySort.BankAccount)]
        [TestCase((BeneficiarySort)4)]
        public void SortBeneficiaries_ReturnsCorrectAssortedCollectionWhenSortingAscending(BeneficiarySort attributeSort)
        {
            IQueryable<Beneficiary> beneficiaries = context
                .Beneficiaries
                .AsQueryable();

            var testModel = new AllBeneficiariesViewModel
            {
                SortAttribute = attributeSort,
                SortBy = SortBy.Ascending,
                BankAccountFilter = "",
                AddressFilter = "",
                IdentifierFilter = "",
                NameFilter = ""
            };

            var result = sorter.SortBeneficiaries(beneficiaries, testModel);

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.First().Identifier, Is.EqualTo("0"));
            Assert.That(result.Last().Identifier, Is.EqualTo("2"));
            Assert.That(result.First().Name, Is.EqualTo("0"));
            Assert.That(result.Last().Name, Is.EqualTo("2"));
            Assert.That(result.First().BankAccount, Is.EqualTo("0"));
            Assert.That(result.Last().BankAccount, Is.EqualTo("2"));
            Assert.That(result.First().Address, Is.EqualTo("0"));
            Assert.That(result.Last().Address, Is.EqualTo("2"));
        }

        [Test]
        [TestCase(EmployeeSort.TotalIncome)]
        [TestCase(EmployeeSort.Salary)]
        [TestCase(EmployeeSort.FirstName)]
        [TestCase(EmployeeSort.LastName)]
        [TestCase(EmployeeSort.DateEmployed)]
        [TestCase(EmployeeSort.Egn)]
        [TestCase((EmployeeSort)6)]
        public void SortEmployees_ReturnsCorrectAssortedCollectionWhenSortingDescending(EmployeeSort attributeSort)
        {
            var employees = context.Employees
                .Select(e => new EmployeeViewModel
                {
                    DateEmployed = e.DateEmployed,
                    Egn = e.Egn,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    MonthlySalary = e.MonthlySalary,
                    TotalIncome = e.MonthlySalary
                })
                .AsQueryable();

            var testModel = new AllEmployeesViewModel
            {
                FirstName = "",
                LastName = "",
                SortAttribute = attributeSort,
                SortBy = SortBy.Descending,
                Egn = ""
            };

            var result = sorter.SortEmployees(employees, testModel);
            if ((int)attributeSort < 6)
            {
                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result.First().DateEmployed, Is.EqualTo(new DateTime(2023, 1, 3)));
                Assert.That(result.Last().DateEmployed, Is.EqualTo(new DateTime(2023, 1, 1)));
                Assert.That(result.First().FirstName, Is.EqualTo("2"));
                Assert.That(result.Last().FirstName, Is.EqualTo("0"));
                Assert.That(result.First().LastName, Is.EqualTo("2"));
                Assert.That(result.Last().LastName, Is.EqualTo("0"));
                Assert.That(result.First().MonthlySalary, Is.EqualTo(2));
                Assert.That(result.Last().MonthlySalary, Is.EqualTo(0));
                Assert.That(result.First().Egn, Is.EqualTo("2"));
                Assert.That(result.Last().Egn, Is.EqualTo("0"));
            }
            else
            {
                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result.First().DateEmployed, Is.EqualTo(new DateTime(2023, 1, 1)));
                Assert.That(result.Last().DateEmployed, Is.EqualTo(new DateTime(2023, 1, 3)));
                Assert.That(result.First().FirstName, Is.EqualTo("0"));
                Assert.That(result.Last().FirstName, Is.EqualTo("2"));
                Assert.That(result.First().LastName, Is.EqualTo("0"));
                Assert.That(result.Last().LastName, Is.EqualTo("2"));
                Assert.That(result.First().MonthlySalary, Is.EqualTo(0));
                Assert.That(result.Last().MonthlySalary, Is.EqualTo(2));
                Assert.That(result.First().Egn, Is.EqualTo("0"));
                Assert.That(result.Last().Egn, Is.EqualTo("2"));
            }
        }

        [Test]
        [TestCase(EmployeeSort.TotalIncome)]
        [TestCase(EmployeeSort.Salary)]
        [TestCase(EmployeeSort.FirstName)]
        [TestCase(EmployeeSort.LastName)]
        [TestCase(EmployeeSort.DateEmployed)]
        [TestCase(EmployeeSort.Egn)]
        [TestCase((EmployeeSort)6)]
        public void SortEmployees_ReturnsCorrectAssortedCollectionWhenSortingAscending(EmployeeSort attributeSort)
        {
            var employees = context.Employees
                .Select(e => new EmployeeViewModel
                {
                    DateEmployed = e.DateEmployed,
                    Egn = e.Egn,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    MonthlySalary = e.MonthlySalary,
                    TotalIncome = e.MonthlySalary
                })
                .AsQueryable();

            var testModel = new AllEmployeesViewModel
            {
                FirstName = "",
                LastName = "",
                SortAttribute = attributeSort,
                SortBy = SortBy.Ascending,
                Egn = ""
            };

            var result = sorter.SortEmployees(employees, testModel);

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.First().DateEmployed, Is.EqualTo(new DateTime(2023, 1, 1)));
            Assert.That(result.Last().DateEmployed, Is.EqualTo(new DateTime(2023, 1, 3)));
            Assert.That(result.First().FirstName, Is.EqualTo("0"));
            Assert.That(result.Last().FirstName, Is.EqualTo("2"));
            Assert.That(result.First().LastName, Is.EqualTo("0"));
            Assert.That(result.Last().LastName, Is.EqualTo("2"));
            Assert.That(result.First().MonthlySalary, Is.EqualTo(0));
            Assert.That(result.Last().MonthlySalary, Is.EqualTo(2));
            Assert.That(result.First().Egn, Is.EqualTo("0"));
            Assert.That(result.Last().Egn, Is.EqualTo("2"));
        }

        [Test]
        [TestCase(AssetSort.Name)]
        [TestCase(AssetSort.AmortizationValue)]
        [TestCase(AssetSort.ReportValue)]
        [TestCase(AssetSort.BalanceValue)]
        [TestCase(AssetSort.DateAquired)]
        [TestCase((AssetSort)5)]
        public void SortAssets_ReturnsCorrectAssortedCollectionWhenSortingDescending(AssetSort sortAttribute)
        {
            var result = sorter.SortAssets(assetTestModel, sortAttribute, SortBy.Descending);

            if ((int)sortAttribute < 5)
            {
                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result.First().Name, Is.EqualTo("C"));
                Assert.That(result.Last().Name, Is.EqualTo("A"));
            }
            else
            {
                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result.First().Name, Is.EqualTo("A"));
                Assert.That(result.Last().Name, Is.EqualTo("C"));
            }
        }

        [Test]
        [TestCase(AssetSort.Name)]
        [TestCase(AssetSort.AmortizationValue)]
        [TestCase(AssetSort.ReportValue)]
        [TestCase(AssetSort.BalanceValue)]
        [TestCase(AssetSort.DateAquired)]
        [TestCase((AssetSort)5)]
        public void SortAssets_ReturnsCorrectAssortedCollectionWhenSortingAscending(AssetSort sortAttribute)
        {
            var result = sorter.SortAssets(assetTestModel, sortAttribute, SortBy.Ascending);

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.First().Name, Is.EqualTo("A"));
            Assert.That(result.Last().Name, Is.EqualTo("C"));
        }

        [Test]
        [TestCase(InformationSort.Date)]
        [TestCase(InformationSort.ReceiverName)]
        [TestCase(InformationSort.Amount)]
        [TestCase(InformationSort.Type)]
        [TestCase(InformationSort.Description)]
        [TestCase((InformationSort)5)]
        public void SortInformationResults_ReturnsCorrectAssortedCollectionWhenSortingDescending(InformationSort attributeSort)
        {
            var payments = context.Payments
                .Select(p => new PaymentInformationItemModel
                {
                    Amount = p.Amount,
                    Date = p.Date,
                    Description = p.Description,
                    ParagraphType = p.Paragraph,
                    PaymentType = p.PaymentType,
                    ReceiverName = p.ReceiverName
                })
                .AsQueryable();

            var testModel = new PaymentInformationViewModel()
            {
                SortBy = SortBy.Descending,
                InformationSort = attributeSort,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 2, 2),
                AmountMin = 1,
                AmountMax = 5,
                ReceiverNameFilter = ""
            };

            var result = sorter.SortInformationResults(payments, testModel);

            if ((int)attributeSort < 5)
            {
                Assert.IsNotNull(result);
                Assert.That(result.First().Description, Is.EqualTo("C"));
                Assert.That(result.Last().Description, Is.EqualTo("A"));
            }
            else
            {
                Assert.IsNotNull(result);
                Assert.That(result.First().Description, Is.EqualTo("A"));
                Assert.That(result.Last().Description, Is.EqualTo("C"));
            }
        }

        [Test]
        [TestCase(InformationSort.Date)]
        [TestCase(InformationSort.ReceiverName)]
        [TestCase(InformationSort.Amount)]
        [TestCase(InformationSort.Type)]
        [TestCase(InformationSort.Description)]
        [TestCase((InformationSort)5)]
        public void SortInformationResults_ReturnsCorrectAssortedCollectionWhenSortingAscending(InformationSort attributeSort)
        {
            var payments = context.Payments
                .Select(p => new PaymentInformationItemModel
                {
                    Amount = p.Amount,
                    Date = p.Date,
                    Description = p.Description,
                    ParagraphType = p.Paragraph,
                    PaymentType = p.PaymentType,
                    ReceiverName = p.ReceiverName
                })
                .AsQueryable();

            var testModel = new PaymentInformationViewModel()
            {
                SortBy = SortBy.Ascending,
                InformationSort = attributeSort,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 2, 2),
                AmountMin = 1,
                AmountMax = 5,
                ReceiverNameFilter = ""
            };

            var result = sorter.SortInformationResults(payments, testModel);

            Assert.IsNotNull(result);
            Assert.That(result.First().Description, Is.EqualTo("A"));
            Assert.That(result.Last().Description, Is.EqualTo("C"));
        }

        [Test]
        public void SortInformationResults_FiltersByNameCorrectly()
        {
            var payments = context.Payments
            .Select(p => new PaymentInformationItemModel
            {
                Amount = p.Amount,
                Date = p.Date,
                Description = p.Description,
                ParagraphType = p.Paragraph,
                PaymentType = p.PaymentType,
                ReceiverName = p.ReceiverName
            })
            .AsQueryable();

            var testModel = new PaymentInformationViewModel()
            {
                SortBy = SortBy.Ascending,
                InformationSort = InformationSort.ReceiverName,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 2, 2),
                AmountMin = 1,
                AmountMax = 5,
                ReceiverNameFilter = "A"
            };

            var result = sorter.SortInformationResults(payments, testModel);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Description, Is.EqualTo("A"));
        }
    }
}
