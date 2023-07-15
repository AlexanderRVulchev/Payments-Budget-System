using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;


namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Contracts;
    using Core.Models.Assets;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Controllers;

    using static Common.ExceptionMessages.Beneficiary;
    using static Common.ExceptionMessages.Payment;

    [TestFixture]
    internal class AssetPaymentControllerTests : ControllerTestBase
    {
        private AssetPaymentController controller;

        private Mock<IBeneficiaryService> mockBeneficiaryService;
        private Mock<IPaymentService> mockPaymentService;

        private Guid beneficiaryId = Guid.NewGuid();
        private Guid paymentId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            mockBeneficiaryService = new Mock<IBeneficiaryService>();

            mockBeneficiaryService
                .Setup(s => s.GetBeneficiaryAsync(testUserId, It.IsAny<Guid>()))
                .ReturnsAsync(GetDefaultTestBeneficiaryFormModel());

            mockPaymentService = new Mock<IPaymentService>();

            mockPaymentService
                .Setup(s => s.AddNewAssetPaymentAsync(testUserId, GetDefaultTestNewAssetFormModel()))
                .ReturnsAsync(paymentId);

            mockPaymentService
                .Setup(s => s.GetAssetPaymentDetailsByIdAsync(testUserId, paymentId))
                .ReturnsAsync(new AssetPaymentDetailsViewModel());

            controller = new AssetPaymentController(mockBeneficiaryService.Object, mockPaymentService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(), 
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Payment_HttpGet_ReturnsViewWithCorrectModel()
        {
            ParagraphType paragraphType = ParagraphType.UpkeepLongTermAssets5100;

            var result = await controller.Payment(beneficiaryId, paragraphType);
            var viewResult = result as ViewResult;

            var expectedModel = new NewAssetFormModel
            {
                BeneficiaryId = beneficiaryId,
                ParagraphType = paragraphType,
                Beneficiary = GetDefaultTestBeneficiaryFormModel()
            };

            Assert.NotNull(viewResult);

            base.AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_HttpGet_RedirectsToErrorIfIdIsNull()
        {
            ParagraphType paragraphType = ParagraphType.UpkeepLongTermAssets5100;

            var result = await controller.Payment(null, paragraphType);
            var redirectResult = result as RedirectToActionResult;

            base.AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        [Test]
        public async Task Payment_HttpGet_RedirectsToErrorIfTypeIsInvalid()
        {
            var result = await controller.Payment(beneficiaryId, null);
            var redirectResult = result as RedirectToActionResult;

            base.AssertRedirectToError(redirectResult, InvalidParagraph);
        }

        [Test]
        public async Task Payment_HttpGet_RedirectsToErrorIfBeneficiaryIsNotFound()
        {
            ParagraphType paragraphType = ParagraphType.UpkeepLongTermAssets5100;

            mockBeneficiaryService
                 .Setup(s => s.GetBeneficiaryAsync(testUserId, beneficiaryId))
                 .ReturnsAsync(() => null!);

            var result = await controller.Payment(beneficiaryId, paragraphType);
            var redirectResult = result as RedirectToActionResult;
                        
            base.AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        [Test]
        public async Task Payment_HttpPost_RedirectsToDetailsIfPaymentIsSuccessful()
        {
            var testModel = GetDefaultTestNewAssetFormModel();

            var result = await controller.Payment(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("AssetPaymentDetails"));
        }

        [Test]
        public async Task Payment_HttpPost_AddsModelErrorsForUndesiredModelData()
        {
            var testModel = GetDefaultTestNewAssetFormModel();

            testModel.InvoiceDate = new DateTime(9999, 12, 31);
            testModel.Position1SingleAssetValue = -1;
            testModel.Position1Quantity = 1;
            testModel.Position1Name = null;

            var result = await controller.Payment(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.That(viewResult.ViewData.ModelState.ErrorCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Payment_HttpPost_AddsModelErrorIfPaymentFailed()
        {
            var testModel = GetDefaultTestNewAssetFormModel();

            mockPaymentService
               .Setup(s => s.AddNewAssetPaymentAsync(testUserId, testModel))
               .ThrowsAsync(new ArgumentException());

            var expectedModel = GetDefaultTestNewAssetFormModel();
            expectedModel.Beneficiary = GetDefaultTestBeneficiaryFormModel();

            var result = await controller.Payment(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.That(viewResult.ViewData.ModelState.ErrorCount, Is.EqualTo(1));

            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Payment_HttpPost_RedirectsToErrorInCaseOfExceptionRelatedToBeneficiary()
        {
            var testModel = GetDefaultTestNewAssetFormModel();

            mockPaymentService
               .Setup(s => s.AddNewAssetPaymentAsync(testUserId, testModel))
               .ThrowsAsync(new InvalidOperationException(BeneficiaryDoesNotExist));

            var result = await controller.Payment(testModel);
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        [Test]
        public async Task AssetPaymentDetails_ReturnsViewWithCorrectModel()
        {
            var expectedModel = new AssetPaymentDetailsViewModel();

            var result = await controller.AssetPaymentDetails(paymentId);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task AssetPaymentDetails_RedirectsToErrorInCaseOfInvalidPaymentId()
        {
            var invalidPaymentId = Guid.NewGuid();

            mockPaymentService
               .Setup(s => s.GetAssetPaymentDetailsByIdAsync(testUserId, invalidPaymentId))
               .ThrowsAsync(new InvalidOperationException(InvalidPayment));

            var result = await controller.AssetPaymentDetails(invalidPaymentId);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            AssertRedirectToError(redirectResult, InvalidPayment);
        }
    }
}
