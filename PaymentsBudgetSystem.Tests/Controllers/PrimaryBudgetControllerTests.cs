using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Areas.Budget.Controllers;
    using Core.Contracts;
    using Core.Models.Budget;

    using static Common.ExceptionMessages.Budget;

    [TestFixture]
    internal class PrimaryBudgetControllerTests : ControllerTestBase
    {
        private PrimaryController controller;

        private Mock<IBudgetService> mockBudgetService;

        [SetUp]
        public void Setup()
        {
            mockBudgetService = new();

            mockBudgetService
                .Setup(s => s.GetIndividualBudgetsAsync(testUserId))
                .ReturnsAsync(GetDefaultListOfBudgetViewModel());

            mockBudgetService
                .Setup(s => s.GetConsolidatedBudgetsAsync(testUserId))
                .ReturnsAsync(GetDefaultListOfConsolidatedBudgetViewModel());

            mockBudgetService
                .Setup(s => s.GetFullConsolidatedBudgetForPrimaryAsync(testUserId, It.IsAny<int>()))
                .ReturnsAsync(GetDefaultEditBudgetFormModel());

            controller = new PrimaryController(mockBudgetService.Object)
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
            var expectedModel = GetDefaultPrimaryBudgetsViewModel();

            var result = await controller.Info();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Info_OnPost_ReturnsViewWithCorrectModelIfModelStateIsInvalid()
        {
            controller.ModelState.AddModelError("", "");

            var testModel = GetDefaultPrimaryBudgetsViewModel();
            var expectedModel = GetDefaultPrimaryBudgetsViewModel();

            var result = await controller.Info(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Info_OnPost_RedirectsToInfoIfNewBudgetIsAddedSuccessfully()
        {
            var testModel = GetDefaultPrimaryBudgetsViewModel();

            var result = await controller.Info(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.That(controller.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.FirstOrDefault().Value, Is.EqualTo("Успешно е добавен нов бюджет!"));
        }

        [Test]
        public async Task Info_OnPost_ReturnsViewWithCorrectModelAndModelErrorIfTheBudgetAlreadyExists()
        {
            mockBudgetService
                .Setup(s => s.AddConsolidatedBudgetAsync(testUserId, 0, 0))
                .ThrowsAsync(new InvalidOperationException(TheBudgetAlreadyExists));

            var testModel = GetDefaultPrimaryBudgetsViewModel();

            var result = await controller.Info(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(controller.ModelState.IsValid);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task EditBudget_OnGet_ReturnsViewWithCorrectModelIfTheConsolidatedBudgetExists()
        {
            int testYear = 0;

            var testModel = GetDefaultEditBudgetFormModel();

            var result = await controller.EditBudget(testYear);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task EditBudget_OnGet_RedirectsToErrorIfConsolidatedBudgetDoesNotExist()
        {
            int testYear = 0;

            mockBudgetService
                .Setup(s => s.GetFullConsolidatedBudgetForPrimaryAsync(testUserId, It.IsAny<int>()))
                .ThrowsAsync(new InvalidOperationException(CannotRetrieveConsolidatedBudget));

            var result = await controller.EditBudget(testYear);
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult, CannotRetrieveConsolidatedBudget);
        }

        [Test]
        public async Task EditBudget_OnPost_ReturnsViewWithCorrectModelIfModelStateIsInvalid()
        {
            controller.ModelState.AddModelError("", "");

            var testModel = GetDefaultEditBudgetFormModel();

            var result = await controller.EditBudget(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task EditBudget_OnPost_ReturnsViewWithCorrectModelAndModelErrorIfTheAllocatedFundsExceedTheLimit()
        {
            var testModel = GetDefaultEditBudgetFormModel();
            testModel.NewSalaryLimit = 100000;

            var expectedModel = GetDefaultEditBudgetFormModel();

            var result = await controller.EditBudget(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task EditBudget_OnPost_ReturnsViewWithCorrectModelAndModelErrorIfTheNewLimitIsLessThanTheExpensesMade()
        {
            var testModel = GetDefaultEditBudgetFormModel();
            testModel.NewSupportLimit = 1;

            var expectedModel = GetDefaultEditBudgetFormModel();

            var result = await controller.EditBudget(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task EditBudget_OnPost_ReturnsViewWithCorrectModelAndMessageIfEditingIsSuccessful()
        {
            var testModel = GetDefaultEditBudgetFormModel();
            testModel.NewSupportLimit = 2000;

            var expectedModel = GetDefaultEditBudgetFormModel();

            var result = await controller.EditBudget(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.That(viewResult.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(viewResult.TempData.FirstOrDefault().Value, Is.EqualTo("Бюджетът е променен успешно!"));
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task EditBudget_OnPost_AddsModelErrorAndReturnsViewWithCorrectModelIfTheBudgetForEditingDoesNotExist()
        {
            mockBudgetService
                .Setup(s => s.EditBudgetAsync(It.IsAny<EditBudgetFormModel>()))
                .ThrowsAsync(new InvalidOperationException(CannotRetrieveIndividualBudget));

            var testModel = GetDefaultEditBudgetFormModel();
            testModel.NewSupportLimit = 2000;

            var expectedModel = GetDefaultEditBudgetFormModel();

            var result = await controller.EditBudget(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(controller.ModelState.IsValid);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }
    }
}
