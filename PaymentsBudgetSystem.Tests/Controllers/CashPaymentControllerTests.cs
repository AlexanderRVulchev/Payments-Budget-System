using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Contracts;
    using Core.Models.Cash;
    using PaymentsBudgetSystem.Controllers;

    using static Common.ExceptionMessages.Employee;
    using static Common.ExceptionMessages.Payment;

    [TestFixture]
    internal class CashPaymentControllerTests : ControllerTestBase
    {
        private CashPaymentController controller;

        private Mock<IEmployeeService> mockEmployeeService;
        private Mock<IPaymentService> mockPaymentService;

        private Guid testEmployeeId;
        private Guid testPaymentId;

        [SetUp]
        public void Setup()
        {
            testEmployeeId = Guid.NewGuid();
            testPaymentId = Guid.NewGuid();

            mockEmployeeService = new();
            mockPaymentService = new();

            mockEmployeeService
                .Setup(s => s.GetEmployeeListAsync(testUserId))
                .ReturnsAsync(GetDefaultListOfEmployeeListModel());

            mockPaymentService
                .Setup(s => s.AddNewCashPaymentAsync(testUserId, It.IsAny<CashPaymentViewModel>()))
                .ReturnsAsync(testPaymentId);

            mockPaymentService
                .Setup(s => s.GetCashPaymentByIdAsync(testUserId, testPaymentId))
                .ReturnsAsync(GetDefaultCashPaymentDetailsModel());

            controller = new CashPaymentController(mockEmployeeService.Object, mockPaymentService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Payment_OnGet_ReturnsViewWithCorrectModel()
        {
            var expectedModel = new CashPaymentViewModel
            {
                Employees = GetDefaultListOfEmployeeListModel()
            };

            var result = await controller.Payment();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_OnPost_ReturnsViewWithCorrectModelIfModelStateIsInvalid()
        {
            var expectedModel = GetDefaultCashPaymentViewModel();

            controller.ModelState.AddModelError("", "");

            var result = await controller.Payment(GetDefaultCashPaymentViewModel());
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_OnPost_RedirectsToCashPaymentDetailsOnSuccessfulPayment()
        {
            var result = await controller.Payment(GetDefaultCashPaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("CashPaymentDetails"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.First().Key, Is.EqualTo("id"));
            Assert.That(redirectResult.RouteValues.First().Value, Is.EqualTo(testPaymentId));
            Assert.That(controller.TempData.First().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.First().Value, Is.EqualTo("Касовото плащане е извършено успешно!"));
        }

        [Test]
        public async Task Payment_OnPost_AddsModelErrorAndReturnsViewInCaseSelectedEmployeeDoesntExist()
        {
            var expectedModel = GetDefaultCashPaymentViewModel();

            mockPaymentService
                .Setup(s => s.AddNewCashPaymentAsync(testUserId, It.IsAny<CashPaymentViewModel>()))
                .ThrowsAsync(new ArgumentException(EmployeeDoesNotExist));

            var result = await controller.Payment(GetDefaultCashPaymentViewModel());
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.That(controller.ModelState.ErrorCount, Is.EqualTo(1));
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_OnPost_RedirectsToErrorIfTheIsNoBudgetCreated()
        {
            mockPaymentService
                .Setup(s => s.AddNewCashPaymentAsync(testUserId, It.IsAny<CashPaymentViewModel>()))
                .ThrowsAsync(new InvalidOperationException(NoBudgetCreated));

            var result = await controller.Payment(GetDefaultCashPaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult, NoBudgetCreated);
        }

        [Test]
        public async Task CashPaymentDetails_ReturnsViewWithCorrectModelIfPaymentIdIsValid()
        {
            var expectedModel = GetDefaultCashPaymentDetailsModel();

            var result = await controller.CashPaymentDetails(testPaymentId);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task CashPaymentDetails_RedirectsToErrorIfPaymentIdIsInvalid()
        {
            var invalidPaymentId = Guid.NewGuid();

            mockPaymentService
                .Setup(s => s.GetCashPaymentByIdAsync(testUserId, invalidPaymentId))
                .ThrowsAsync(new InvalidOperationException(InvalidPayment));

            var result = await controller.CashPaymentDetails(invalidPaymentId);
            var viewResult = result as RedirectToActionResult;

            Assert.IsNotNull(viewResult);
            AssertRedirectToError(viewResult, InvalidPayment);

        }


    }
}
