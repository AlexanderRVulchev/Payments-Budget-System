using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;


namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Contracts;
    using Core.Models.Salaries;
    using PaymentsBudgetSystem.Controllers;

    using static Common.ExceptionMessages.Payment;

    internal class SalariesControllerTests : ControllerTestBase
    {
        private SalariesController controller;

        private Mock<IPaymentService> mockPaymentService;

        private SalariesPaymentViewModel testModel;

        [SetUp]
        public void Setup()
        {
            testModel = GetDefaultSalariesPaymentViewModel();
            int testYear = 2023;
            int testMonth = 1;

            mockPaymentService = new();

            mockPaymentService
                .Setup(s => s.CreatePayrollAsync(testUserId, testYear, testMonth))
                .ReturnsAsync(GetDefaultSalariesPaymentViewModel());

            mockPaymentService
                .Setup(s => s.AddNewSalariesPaymentAsync(testUserId, It.IsAny<SalariesPaymentViewModel>()))
                .ReturnsAsync(testGuidId);

            mockPaymentService
                .Setup(s => s.GetSalariesDetailsByIdAsync(testUserId, testGuidId))
                .ReturnsAsync(testModel);

            controller = new SalariesController(mockPaymentService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Payment_ReturnsViewWithCorrectModel()
        {
            int testYear = 2023;
            int testMonth = 1;

            var result = await controller.Payment(testYear, testMonth);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task ProcessPayment_ReturnsViewWithCorrectModelIfModelStateIsInvalid()
        {
            controller.ModelState.AddModelError("", "");

            var result = await controller.ProcessPayment(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task ProcessPayment_RedirectsToDetailsWithRouteParameterIfPaymentIsSuccessful()
        {
            var result = await controller.ProcessPayment(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Details"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.First().Key, Is.EqualTo("id"));
            Assert.That(redirectResult.RouteValues.First().Value, Is.EqualTo(testGuidId));
            Assert.That(controller.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.FirstOrDefault().Value, Is.EqualTo("Заплатите са изплатени успешно!"));
        }

        [Test]
        public async Task ProcessPayment_AddsModelErrorAndReturnsViewWithCorrectModelIfPaymentExceedsBudgetLimit()
        {
            mockPaymentService
               .Setup(s => s.AddNewSalariesPaymentAsync(testUserId, It.IsAny<SalariesPaymentViewModel>()))
               .ThrowsAsync(new ArgumentException(PaymentExceedsBudgetLimit));

            var result = await controller.ProcessPayment(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(controller.ModelState.IsValid);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task ProcessPayment_RedirectsToErrorIfNoBudgetIsCreated()
        {
            mockPaymentService
               .Setup(s => s.AddNewSalariesPaymentAsync(testUserId, It.IsAny<SalariesPaymentViewModel>()))
               .ThrowsAsync(new InvalidOperationException(NoBudgetCreated));

            var result = await controller.ProcessPayment(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, NoBudgetCreated);
        }

        [Test]
        public async Task Details_ReturnsViewWithCorrectModelIfPaymentIdIsValid()
        {
            var result = await controller.Details(testGuidId);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task Details_RedirectsToErrorIfPaymentIdIsInvalid()
        {
            mockPaymentService
                .Setup(s => s.GetSalariesDetailsByIdAsync(testUserId, testGuidId))
                .ThrowsAsync(new InvalidOperationException(InvalidPayment));

            var result = await controller.Details(testGuidId);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, InvalidPayment);
        }
    }
}
