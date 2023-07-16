using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Contracts;
    using Core.Models.Beneficiaries;
    using Core.Models.Support;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Controllers;

    using static Common.ExceptionMessages.Beneficiary;
    using static Common.ExceptionMessages.Payment;

    [TestFixture]
    internal class SupportControllerTests : ControllerTestBase
    {
        private SupportController controller;

        private Mock<IPaymentService> mockPaymentService;
        private Mock<IBeneficiaryService> mockBeneficiaryService;

        [SetUp]
        public void Setup()
        {
            mockPaymentService = new();
            mockBeneficiaryService = new();

            mockBeneficiaryService
                .Setup(s => s.GetBeneficiaryAsync(testUserId, testGuidId))
                .ReturnsAsync(GetDefaultBeneficiaryFormModel());

            mockPaymentService
                .Setup(s => s.AddNewSupportPaymentAsync(testUserId, It.IsAny<SupportPaymentFormModel>()))
                .ReturnsAsync(testGuidId);

            mockPaymentService
                .Setup(s => s.GetSupportPaymentDetailsByIdAsync(testUserId, testGuidId))
                .ReturnsAsync(GetDefaultSupportPaymentDetailsViewModel());

            controller = new SupportController(mockBeneficiaryService.Object, mockPaymentService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());

        }

        [Test]
        public async Task Payment_OnGet_RedirectsToErrorIfBeneficiaryIdIsNull()
        {
            var testParagraph = ParagraphType.Materials1015;

            var result = await controller.Payment(null, testParagraph);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        [Test]
        [TestCase(ParagraphType.InsuranceAdditional0580)]
        [TestCase(null)]
        public async Task Payment_OnGet_RedirectsToErrorIfParagraphIsInvalid(ParagraphType? testParagraph)
        {
            var result = await controller.Payment(testGuidId, testParagraph);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, InvalidParagraph);
        }

        [Test]
        public async Task Payment_OnGet_ReturnsViewWithCorrectModelIfPaymentIsSuccessful()
        {
            var testParagraph = ParagraphType.Materials1015;

            var expectedModel = new SupportPaymentFormModel
            {
                Beneficiary = GetDefaultBeneficiaryFormModel(),
                ParagraphType = testParagraph
            };

            var result = await controller.Payment(testGuidId, testParagraph);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_OnGetRedirectsToErrorIfBeneficiaryIdIsInvalid()
        {
            var testParagraph = ParagraphType.Materials1015;

            mockBeneficiaryService
                .Setup(s => s.GetBeneficiaryAsync(testUserId, testGuidId))
                .ReturnsAsync(() => null!);

            var result = await controller.Payment(testGuidId, testParagraph);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        [Test]
        public async Task Payment_OnPost_AddsModelErrorAndReturnsViewWithCorrectModelIfInvoiceDateIsInTheFuture()
        {
            var invoiceDateInTheFuture = new DateTime(9999, 1, 1);

            var testModel = GetDefaultSupportPaymentFormModel();
            testModel.InvoiceDate = invoiceDateInTheFuture;

            var expectedModel = GetDefaultSupportPaymentFormModel();
            expectedModel.InvoiceDate = invoiceDateInTheFuture;
            expectedModel.Beneficiary = GetDefaultBeneficiaryFormModel();

            var result = await controller.Payment(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(controller.ModelState.IsValid);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_OnPost_AddsModelErrorAndReturnsViewWithCorrectModelIfModelAmountIsNegative()
        {
            var negativeAmount = -1;

            var testModel = GetDefaultSupportPaymentFormModel();
            testModel.Amount = negativeAmount;

            var expectedModel = GetDefaultSupportPaymentFormModel();
            expectedModel.Amount = negativeAmount;
            expectedModel.Beneficiary = GetDefaultBeneficiaryFormModel();

            var result = await controller.Payment(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(controller.ModelState.IsValid);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_OnPost_RedirectsToSupportPaymentDetailsWithRouteValueIfPaymentIsSuccessful()
        {
            var testModel = GetDefaultSupportPaymentFormModel();

            var result = await controller.Payment(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("SupportPaymentDetails"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.FirstOrDefault().Key, Is.EqualTo("id"));
            Assert.That(redirectResult.RouteValues.FirstOrDefault().Value, Is.EqualTo(testGuidId));
            Assert.That(controller.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.FirstOrDefault().Value, Is.EqualTo("Плащането е извършено успешно!"));
        }

        [Test]
        public async Task Payment_OnPost_ReturnsViewWithCorrectModelAndAddsModelErrorIfPaymentExceedsBudgetLimit()
        {
            var testModel = GetDefaultSupportPaymentFormModel();

            var expectedModel = GetDefaultSupportPaymentFormModel();
            expectedModel.Beneficiary = new BeneficiaryFormModel
            {
                Id = null,
                Name = ""
            };

            mockPaymentService
                .Setup(s => s.AddNewSupportPaymentAsync(testUserId, It.IsAny<SupportPaymentFormModel>()))
                .ThrowsAsync(new ArgumentException());

            var result = await controller.Payment(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(controller.ModelState.IsValid);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_OnPost_RedirectsToErrorIfBeneficiaryDoesNotExist()
        {
            var testModel = GetDefaultSupportPaymentFormModel();

            mockPaymentService
                .Setup(s => s.AddNewSupportPaymentAsync(testUserId, It.IsAny<SupportPaymentFormModel>()))
                .ThrowsAsync(new InvalidOperationException(BeneficiaryDoesNotExist));

            var result = await controller.Payment(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        [Test]
        public async Task SupportPaymentDetails_ReturnsViewWithCorrectModelIfPaymentIdIsValid()
        {
            var expectedModel = GetDefaultSupportPaymentDetailsViewModel();

            var result = await controller.SupportPaymentDetails(testGuidId);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task SupportPaymentDetails_RedirectsToErrorIfPaymentIdIsInvalid()
        {
            mockPaymentService
                .Setup(s => s.GetSupportPaymentDetailsByIdAsync(testUserId, testGuidId))
                .ThrowsAsync(new InvalidOperationException(InvalidPayment));

            var result = await controller.SupportPaymentDetails(testGuidId);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, InvalidPayment);
        }
    }
}
