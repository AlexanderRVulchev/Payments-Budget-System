using Microsoft.EntityFrameworkCore;
using PaymentsBudgetSystem.Core.Contracts;
using PaymentsBudgetSystem.Core.Models.Employees;
using PaymentsBudgetSystem.Core.Models.Enums;
using PaymentsBudgetSystem.Core.Services;
using PaymentsBudgetSystem.Data;
using PaymentsBudgetSystem.Data.Entities;
using PaymentsBudgetSystem.Data.Entities.Enums;
using GlobalSetting = PaymentsBudgetSystem.Core.Models.Enums.GlobalSetting;

namespace PaymentsBudgetSystem.Tests.Services
{
    [TestFixture]
    internal class EmployeeServiceTests
    {
        private PBSystemDbContext context;

        private IEmployeeService employeeService;

        private string testUserId;

        [SetUp]
        public async Task Setup()
        {
            testUserId = "test user id";

            var employees = new List<Employee>()
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Egn = "0000000000",
                    DateEmployed = new DateTime(2023, 1, 1),
                    DateLeft = null,
                    FirstName = "test first name 1",
                    LastName = "test last name 1",
                    MonthlySalary = 1000,
                    UserId = testUserId
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Egn = "1111111111",
                    DateEmployed = new DateTime(2023, 1, 2),
                    DateLeft = null,
                    FirstName = "test first name 2",
                    LastName = "test last name 2",
                    MonthlySalary = 3000,
                    UserId = testUserId
                }
            };

            var globalSettingForMinimumWage = new Data.Entities.GlobalSetting
            {
                Id = (int)GlobalSetting.MinimumWage,
                SettingName = "Минимална работна заплата",
                SettingValue = 780m
            };

            var options = new DbContextOptionsBuilder<PBSystemDbContext>()
               .UseInMemoryDatabase(databaseName: "PBSystemInMemory")
               .Options;
            context = new PBSystemDbContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Employees.AddRangeAsync(employees);
            await context.GlobalSettings.AddAsync(globalSettingForMinimumWage);

            await context.SaveChangesAsync();

            employeeService = new EmployeeService(context);
        }

        [Test]
        public async Task AddEmployee_CreatesNewEntityProperly()
        {
            var testModel = new EmployeeFormModel
            {
                ContractType = ContractType.StateOfficial,
                DateEmployed = new DateTime(2023, 2, 2),
                MonthlySalary = 4000,
                Egn = "9999999999",
                FirstName = "new test first name",
                LastName = "new test last  name"
            };

            await employeeService
                .AddEmployeeAsync(testUserId, testModel);

            var actualEntity = await context.Employees
                .Where(e => e.ContractType == testModel.ContractType)
                .Where(e => e.DateEmployed == testModel.DateEmployed)
                .Where(e => e.Egn == testModel.Egn)
                .Where(e => e.MonthlySalary == testModel.MonthlySalary)
                .Where(e => e.FirstName == testModel.FirstName)
                .Where(e => e.LastName == testModel.LastName)
                .FirstOrDefaultAsync();

            Assert.That(actualEntity, Is.Not.Null);
        }

        [Test]
        public async Task EditEmployee_ChangesTheEntityCorrectly()
        {
            var testModel = new EmployeeFormModel
            {
                Id = await context.Employees
                    .Select(e => e.Id)
                    .FirstAsync(),
                MonthlySalary = 4000,
                DateLeft = new DateTime(2023, 2, 2),
                Egn = "9999999999",
                FirstName = "new test first name",
                LastName = "new test last  name"
            };

            await employeeService
                .EditEmployeeAsync(testUserId, testModel);

            var actualEntity = await context.Employees
                .Where(e => e.Id == testModel.Id)
                .Where(e => e.DateLeft == testModel.DateLeft)
                .Where(e => e.ContractType == testModel.ContractType)
                .Where(e => e.Egn == testModel.Egn)
                .Where(e => e.MonthlySalary == testModel.MonthlySalary)
                .Where(e => e.FirstName == testModel.FirstName)
                .Where(e => e.LastName == testModel.LastName)
                .FirstOrDefaultAsync();

            Assert.That(actualEntity, Is.Not.Null);
        }

        [Test]
        public void EditEmployee_ThrowsForInvalidEmployeeId()
        {
            var testModel = new EmployeeFormModel();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await employeeService.EditEmployeeAsync(testUserId, testModel));
        }

        [Test]
        public async Task EditEmployee_ThrowsForInvalidUserId()
        {
            string invalidUserId = "invalid user id";
            Guid validEmployeeId = await context.Employees
                .Select(e => e.Id)
                .FirstAsync();

            var testModel = new EmployeeFormModel
            {
                Id = validEmployeeId
            };

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await employeeService.EditEmployeeAsync(invalidUserId, testModel));
        }

        [Test]
        public async Task GetAllEmployeesAsync_ReturnsCorrectModel()
        {
            var testModel = new AllEmployeesViewModel
            {
                NumberOfPages = 1,
                Page = 0
            };

            var result = await employeeService.GetAllEmployeesAsync(testUserId, testModel);

            Assert.That(result.Employees.Count, Is.EqualTo(2));
            Assert.That(result.Page, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllEmployees_ChangedPageAsExpected()
        {
            var testModel = new AllEmployeesViewModel
            {
                NumberOfPages = 1,
                Page = 99
            };

            var result = await employeeService.GetAllEmployeesAsync(testUserId, testModel);

            Assert.That(result.Employees.Count, Is.EqualTo(2));
            Assert.That(result.Page, Is.EqualTo(1));
        }

        [Test]
        public async Task GetEmployee_ReturnsCorrectModel()
        {
            var actualEmployee = await context.Employees.FirstAsync();
            var employeeId = actualEmployee.Id;

            var result = await employeeService.GetEmployeeAsync(testUserId, employeeId);

            Assert.That(result.Id, Is.EqualTo(actualEmployee.Id));
            Assert.That(result.Egn, Is.EqualTo(actualEmployee.Egn));
            Assert.That(result.ContractType, Is.EqualTo(actualEmployee.ContractType));
            Assert.That(result.FirstName, Is.EqualTo(actualEmployee.FirstName));
            Assert.That(result.LastName, Is.EqualTo(actualEmployee.LastName));
            Assert.That(result.DateEmployed, Is.EqualTo(actualEmployee.DateEmployed));
            Assert.That(result.DateLeft, Is.EqualTo(actualEmployee.DateLeft));
            Assert.That(result.MonthlySalary, Is.EqualTo(actualEmployee.MonthlySalary));            
        }

        [Test]
        public void GetEmployee_ThrowsForInvalidEmployeeId()
        {
            var invalidEmployeeId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await employeeService.GetEmployeeAsync(testUserId, invalidEmployeeId));
        }

        [Test]
        public async Task GetEmployee_ThrowsForInvalidUserId()
        {
            var invalidUserId = "invalid user id";
            var employeeId = await context.Employees.Select(e => e.Id).FirstAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await employeeService.GetEmployeeAsync(invalidUserId, employeeId));
        }

        [Test]
        public async Task GetEmployeeList_ReturnsProperCollection()
        {
            var result = await employeeService.GetEmployeeListAsync(testUserId);

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetEmployeeById_ReturnsCorrectEmployeeModel()
        {
            var actualEntity = await context.Employees.FirstAsync();
            var actualEmployeeId = actualEntity.Id;

            var result = await employeeService.GetEmployeeByIdAsync(actualEmployeeId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetEmployeeById_ThrowsForInvalidEmployeeId()
        {
            var invalidEmployeeId = Guid.NewGuid();

            Assert.ThrowsAsync<InvalidOperationException>(async ()
                => await employeeService.GetEmployeeByIdAsync(invalidEmployeeId));
        }

        [Test]
        public async Task GetMinimumWage_ReturnsProperValueForMinimumWageSetting()
        {
            var minimumWage = await context.GlobalSettings.Select(gs => gs.SettingValue).FirstAsync();

            var result = await employeeService.GetMinimumWageAsync();

            Assert.That(result, Is.EqualTo(minimumWage));
        }

        [Test]
        public async Task GetMinimumWage_ReturnsZeroForMissingSetting()
        {
            var settingToRemove = await context.GlobalSettings.FirstAsync();
            context.GlobalSettings.Remove(settingToRemove);
            await context.SaveChangesAsync();

            var result = await employeeService.GetMinimumWageAsync();

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
