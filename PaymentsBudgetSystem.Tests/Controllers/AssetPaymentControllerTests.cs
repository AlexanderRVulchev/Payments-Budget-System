using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using Newtonsoft.Json;
using PaymentsBudgetSystem.Controllers;
using PaymentsBudgetSystem.Core.Contracts;
using PaymentsBudgetSystem.Core.Models.Assets;
using PaymentsBudgetSystem.Core.Models.Beneficiaries;
using PaymentsBudgetSystem.Data.Entities;
using PaymentsBudgetSystem.Data.Entities.Enums;
using System.Security.Claims;

namespace PaymentsBudgetSystem.Tests.Controllers
{

    [TestFixture]
    internal class AssetPaymentControllerTests : ControllerTestBase
    {
        private AssetPaymentController assetPaymentController;

        private Mock<IBeneficiaryService> mockBeneficiaryService;
        private Mock<IPaymentService> mockPaymentService;

        private Guid beneficiaryId = Guid.NewGuid();
        private Guid paymentId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            mockBeneficiaryService = new Mock<IBeneficiaryService>();

            mockBeneficiaryService
                .Setup(s => s.GetBeneficiaryAsync(testUserId, beneficiaryId))
                .ReturnsAsync(GetDefaultTestBeneficiaryFormModel());

            mockPaymentService = new Mock<IPaymentService>();

            mockPaymentService
                .Setup(s => s.AddNewAssetPaymentAsync(testUserId, GetDefaultTestNewAssetFormModel()))
                .ReturnsAsync(paymentId);

            assetPaymentController = new AssetPaymentController(mockBeneficiaryService.Object, mockPaymentService.Object)
            {
                ControllerContext = testControllerContext,
            };

            assetPaymentController.TempData = new TempDataDictionary(
                new DefaultHttpContext(), 
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Payment_HttpGet_ReturnsViewWithCorrectModel()
        {
            ParagraphType paragraphType = ParagraphType.UpkeepLongTermAssets5100;

            var result = await assetPaymentController.Payment(beneficiaryId, paragraphType);
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

            var result = await assetPaymentController.Payment(null, paragraphType);
            var redirectResult = result as RedirectToActionResult;

            base.AssertRedirectToError(redirectResult);
        }

        [Test]
        public async Task Payment_HttpGet_RedirectsToErrorIfTypeIsInvalid()
        {
            var result = await assetPaymentController.Payment(beneficiaryId, null);
            var redirectResult = result as RedirectToActionResult;

            base.AssertRedirectToError(redirectResult);
        }

        [Test]
        public async Task Payment_HttpGet_RedirectsToErrorIfBeneficiaryIsNotFound()
        {
            ParagraphType paragraphType = ParagraphType.UpkeepLongTermAssets5100;

            mockBeneficiaryService
                 .Setup(s => s.GetBeneficiaryAsync(testUserId, beneficiaryId))
                 .ReturnsAsync(() => null!);

            var result = await assetPaymentController.Payment(beneficiaryId, paragraphType);
            var redirectResult = result as RedirectToActionResult;
                        
            base.AssertRedirectToError(redirectResult);
        }

        [Test]
        public async Task Payment_HttpPost_RedirectsToDetailsIfPaymentIsSuccessful()
        {
            var testModel = GetDefaultTestNewAssetFormModel();

            var result = await assetPaymentController.Payment(testModel);
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

            var result = await assetPaymentController.Payment(testModel);
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

            var result = await assetPaymentController.Payment(testModel);
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
               .ThrowsAsync(new InvalidOperationException());

            var result = await assetPaymentController.Payment(testModel);
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult);
        }


        private NewAssetFormModel GetDefaultTestNewAssetFormModel()
            => new NewAssetFormModel
            {
                Beneficiary = new BeneficiaryFormModel
                {
                    Id = beneficiaryId
                },
                Position1SingleAssetValue = 100,
                Position1Name = "asset name",
                Position1Quantity = 1,
                BeneficiaryId = beneficiaryId,
                ParagraphType = ParagraphType.UpkeepLongTermAssets5100,
            };

        private BeneficiaryFormModel GetDefaultTestBeneficiaryFormModel()
            => new BeneficiaryFormModel
            {
                Id = beneficiaryId,
                Address = "",
                BankAccount = "",
                Identifier = "",
                Name = ""
            };
    }
}
