using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;


namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Contracts;
    using Core.Models.Enums;
    using Core.Models.Information;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Controllers;

    [TestFixture]
    internal class InformationControllerTests : ControllerTestBase
    {
        private InformationController controller;

        private Mock<IInformationService> mockInformationService;

        [SetUp]
        public void Setup()
        {
            mockInformationService = new();

            mockInformationService
                .Setup(s => s.GetPaymentsInfoAsync(testUserId, It.IsAny<PaymentInformationViewModel>()))
                .ReturnsAsync(GetDefaultPaymentInformationViewModel());

            controller = new InformationController(mockInformationService.Object)
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
            var testModel = GetDefaultPaymentInformationViewModel();

            DateTime? from = testModel.StartDate;
            DateTime? to = testModel.EndDate;
            SortBy sortBy = testModel.SortBy;
            InformationSort informationSort = testModel.InformationSort;
            PaymentType paymentType = testModel.PaymentType;
            decimal? amountMin = testModel.AmountMin;
            decimal? amountMax = testModel.AmountMax;
            int page = testModel.Page;
            string receiver = testModel.ReceiverNameFilter;

            var result = await controller.Info(from, to, sortBy, informationSort, paymentType, amountMin, amountMax, page, receiver);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task Info_OnGet_AddsModelErrorAndReturnsViewIfEndDateIsEarlierThanStartDate()
        {
            var testModel = GetDefaultPaymentInformationViewModel();

            DateTime? from = testModel.StartDate;
            DateTime? to = new DateTime(2022, 1, 1);
            SortBy sortBy = testModel.SortBy;
            InformationSort informationSort = testModel.InformationSort;
            PaymentType paymentType = testModel.PaymentType;
            decimal? amountMin = testModel.AmountMin;
            decimal? amountMax = testModel.AmountMax;
            int page = testModel.Page;
            string receiver = testModel.ReceiverNameFilter;

            var result = await controller.Info(from, to, sortBy, informationSort, paymentType, amountMin, amountMax, page, receiver);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(controller.ModelState.IsValid);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public void Info_OnPost_RedirectsToInfoWithProperParameters()
        {
            var testModel = GetDefaultPaymentInformationViewModel();

            var result = controller.Info(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.Count, Is.EqualTo(9));
        }

        [Test]
        public void Info_OnPost_ReturnsViewWithCorrectModelIfModelStateIsInvalid()
        {
            controller.ModelState.AddModelError("", "");

            var testModel = GetDefaultPaymentInformationViewModel();

            var result = controller.Info(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }
    }
}
