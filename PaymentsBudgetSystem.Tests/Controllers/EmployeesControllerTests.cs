using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;


namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Contracts;
    using Core.Models.Employees;
    using PaymentsBudgetSystem.Controllers;

    using static Common.ExceptionMessages.Employee;

    internal class EmployeesControllerTests : ControllerTestBase
    {
        private EmployeesController controller;

        private Mock<IEmployeeService> mockEmployeeService;

        private Guid testEmployeeId;

        [SetUp]
        public void Setup()
        {
            testEmployeeId = Guid.NewGuid();

            mockEmployeeService = new();

            mockEmployeeService
                .Setup(s => s.GetAllEmployeesAsync(testUserId, It.IsAny<AllEmployeesViewModel>()))
                .ReturnsAsync(GetDefaultAllEmployeesViewModel());

            mockEmployeeService
                .Setup(s => s.GetMinimumWageAsync())
                .ReturnsAsync(780);

            mockEmployeeService
                .Setup(s => s.GetEmployeeAsync(testUserId, testEmployeeId))
                .ReturnsAsync(GetDefaultEmployeeFormModel());

            controller = new EmployeesController(mockEmployeeService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Info_OnGet_ReturnsViewWithCorrectModel()
        {
            var expectedModel = GetDefaultAllEmployeesViewModel();

            var result = await controller.Info(
                expectedModel.FirstName,
                expectedModel.LastName,
                expectedModel.Egn,
                (int)expectedModel.SortAttribute,
                (int)expectedModel.SortBy,
                expectedModel.Page);

            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public void Info_OnPost_RedirectsToInfoWithCorrectNumberOfRouteValues()
        {
            var result = controller.Info(GetDefaultAllEmployeesViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.Count, Is.EqualTo(6));
        }

        [Test]
        public void Add_OnGet_ReturnsViewWithCorrectModel()
        {
            var result = controller.Add();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, new EmployeeFormModel());
        }

        [Test]
        public async Task Add_OnPost_RedirectsToInfoIfTheAddOperationIsSuccessful()
        {
            var result = await controller.Add(GetDefaultEmployeeFormModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.That(controller.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.FirstOrDefault().Value, Is.EqualTo("Успешно добавяне на нов служител!"));
        }

        [Test]
        public async Task Add_OnPost_AddsModelErrorIfTheEmployeesSalaryIsBelowTheMinimumWageAndReturnsView()
        {
            var employeeSalaryBelowTheMinimumWage = 100;

            var testModel = GetDefaultEmployeeFormModel();
            testModel.MonthlySalary = employeeSalaryBelowTheMinimumWage;

            var result = await controller.Add(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [Test]
        public async Task Edit_OnGet_ReturnsViewIfEmployeeIdIsValid()
        {
            var result = await controller.Edit(testEmployeeId);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, GetDefaultEmployeeFormModel());
        }

        [Test]
        public async Task Edit_OnGet_RedirectsToErrorIfEmployeeIdIsInvalid()
        {
            var invalidEmployeeId = Guid.NewGuid();

            mockEmployeeService
               .Setup(s => s.GetEmployeeAsync(testUserId, invalidEmployeeId))
               .ThrowsAsync(new InvalidOperationException(EmployeeDoesNotExist));

            var result = await controller.Edit(invalidEmployeeId);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, EmployeeDoesNotExist);
        }

        [Test]
        public async Task Edit_OnPost_RedirectsToInfoIfEditingIsSuccessful()
        {
            var result = await controller.Edit(GetDefaultEmployeeFormModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.That(controller.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.FirstOrDefault().Value, Is.EqualTo("Успешна редакция на служител!"));
        }

        [Test]
        public async Task Edit_OnPost_ReturnsViewWithCorrectModelIfDateEmployedIsAfterDateLeft()
        {
            var testModel = GetDefaultEmployeeFormModel();
            testModel.DateEmployed = new DateTime(2023, 1, 2);
            testModel.DateLeft = new DateTime(2023, 1, 1);

            var result = await controller.Edit(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task Edit_OnPost_RedirectsToErrorIfEmployeeIdIsInvalid()
        {
            var testModel = GetDefaultEmployeeFormModel();

            mockEmployeeService
               .Setup(s => s.EditEmployeeAsync(testUserId, testModel))
               .ThrowsAsync(new InvalidOperationException(EmployeeDoesNotExist));

            var result = await controller.Edit(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, EmployeeDoesNotExist);
        }
    }
}
