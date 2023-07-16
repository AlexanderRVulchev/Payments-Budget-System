using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PaymentsBudgetSystem.Areas.Budget.Controllers;
using PaymentsBudgetSystem.Core.Contracts;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    internal class SecondaryBudgetControllerTests : ControllerTestBase
    {
        private SecondaryController controller;

        private Mock<IBudgetService> mockBudgetService;

        [SetUp]
        public void Setup()
        {
            mockBudgetService = new();

            mockBudgetService
                .Setup(s => s.GetIndividualBudgetsAsync(testUserId))
                .ReturnsAsync(GetDefaultListOfBudgetViewModel());

            controller = new SecondaryController(mockBudgetService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());

        }

        [Test]
        public async Task Info_ReturnsViewWithCorrectModel()
        {
            var expectedModel = GetDefaultListOfBudgetViewModel();

            var result = await controller.Info();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }
    }
}
