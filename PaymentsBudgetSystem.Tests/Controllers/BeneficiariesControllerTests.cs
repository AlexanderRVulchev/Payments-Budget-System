using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    using PaymentsBudgetSystem.Controllers;
    using Core.Contracts;
    using Core.Models.Beneficiaries;

    using static Common.ExceptionMessages.Beneficiary;

    [TestFixture]
    internal class BeneficiariesControllerTests : ControllerTestBase
    {
        private BeneficiariesController beneficiariesController;

        private Mock<IBeneficiaryService> mockBeneficiaryService;

        private Guid testBeneficiaryId;

        [SetUp]
        public void Setup()
        {
            testBeneficiaryId = Guid.NewGuid();
            mockBeneficiaryService = new();

            mockBeneficiaryService
                .Setup(s => s.GetAllBeneficiariesAsync(testUserId, It.IsAny<AllBeneficiariesViewModel>()))
                .ReturnsAsync(GetDefaultAllBeneficiariesViewModel());

            mockBeneficiaryService
                .Setup(s => s.GetBeneficiaryAsync(testUserId, testBeneficiaryId))
                .ReturnsAsync(GetDefaultBeneficiaryFormModel());

            beneficiariesController = new BeneficiariesController(mockBeneficiaryService.Object)
            {
                ControllerContext = testControllerContext
            };

            beneficiariesController.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Info_OnGet_ReturnsViewWithCorrectModel()
        {
            var testModel = GetDefaultAllBeneficiariesViewModel();

            var result = await beneficiariesController.Info(
                testModel.NameFilter, 
                testModel.IdentifierFilter,
                testModel.AddressFilter, 
                testModel.BankAccountFilter,
                (int)testModel.SortBy, 
                (int)testModel.SortAttribute,
                testModel.Page);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public void Info_OnPost_RedirectsWithProperArguments()
        {
            var testModel = GetDefaultAllBeneficiariesViewModel();

            var result = beneficiariesController.Info(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.Count, Is.EqualTo(7));
        }

        [Test]
        public void Add_OnGet_ReturnsViewWithProperModel()
        {
            var result = beneficiariesController.Add();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, new BeneficiaryFormModel());
        }

        [Test]
        public async Task Add_OnPost_ReturnsViewWithProperModelIfModelStateIsInvalid()
        {
            var testModel = GetDefaultBeneficiaryFormModel();

            beneficiariesController.ModelState.AddModelError("", "");

            var result = await beneficiariesController.Add(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task Add_OnPost_RedirectsToInfoViewOnSuccessfulAdd()
        {
            var testModel = GetDefaultBeneficiaryFormModel();

            var result = await beneficiariesController.Add(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(beneficiariesController.TempData.First().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(beneficiariesController.TempData.First().Value, Is.EqualTo("Успешно добавяне на контрагент!"));
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
        }

        [Test]
        public async Task Add_OnPost_AddsModelErrorAndReturnsViewIfAddOperationFails()
        {
            var testModel = GetDefaultBeneficiaryFormModel();

            mockBeneficiaryService
                .Setup(s => s.AddBeneficiaryAsync(testUserId, It.IsAny<BeneficiaryFormModel>()))
                .ThrowsAsync(new InvalidOperationException(BeneficiaryAlreadyExists));

            var result = await beneficiariesController.Add(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(beneficiariesController.ModelState.IsValid);
            Assert.That(beneficiariesController.ModelState.ErrorCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Edit_OnGet_ReturnsViewWithCorrectModelOnValidId()
        {
            var expectedModel = GetDefaultBeneficiaryFormModel();

            var result = await beneficiariesController.Edit(testBeneficiaryId);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Edit_OnGet_RedirectsToErrorInCaseOfInvalidBeneficiaryId()
        {
            var invalidBeneficiaryId = Guid.NewGuid();

            mockBeneficiaryService
                .Setup(s => s.GetBeneficiaryAsync(testUserId, invalidBeneficiaryId))
                .ThrowsAsync(new InvalidOperationException(BeneficiaryDoesNotExist));

            var result = await beneficiariesController.Edit(invalidBeneficiaryId);
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        [Test]
        public async Task Edit_OnPost_ReturnsViewWithCorrectModelIfModelStateIsInvalid()
        {
            var expectedModel = GetDefaultBeneficiaryFormModel();

            beneficiariesController.ModelState.AddModelError("", "");

            var result = await beneficiariesController.Edit(GetDefaultBeneficiaryFormModel());
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Edit_OnPost_RedirectsToActionInfoIfEditIsSuccessful()
        {
            var result = await beneficiariesController.Edit(GetDefaultBeneficiaryFormModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.That(beneficiariesController.TempData.First().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(beneficiariesController.TempData.First().Value, Is.EqualTo("Контрагентът е редактиран успешно!"));
        }

        [Test]
        public async Task Edit_OnPost_RedirectsToErrorIfBeneficiaryIdIsInvalid()
        {
            mockBeneficiaryService
                .Setup(s => s.EditBeneficiaryAsync(testUserId, It.IsAny<BeneficiaryFormModel>()))
                .ThrowsAsync(new InvalidOperationException(BeneficiaryDoesNotExist));

            var result = await beneficiariesController.Edit(GetDefaultBeneficiaryFormModel());
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult, BeneficiaryDoesNotExist);
        }

        private AllBeneficiariesViewModel GetDefaultAllBeneficiariesViewModel()
            => new AllBeneficiariesViewModel
            {
                AddressFilter = "",
                BankAccountFilter = "",
                IdentifierFilter = "",
                NameFilter = "",
                Page = 1,
            };

        private BeneficiaryFormModel GetDefaultBeneficiaryFormModel()
            => new BeneficiaryFormModel
            {
                Address = "",
                BankAccount = "",
                Identifier = "",
                Name = ""
            };
    }
}
