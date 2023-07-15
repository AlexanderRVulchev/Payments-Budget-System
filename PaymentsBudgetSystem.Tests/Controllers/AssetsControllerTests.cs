using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;


namespace PaymentsBudgetSystem.Tests.Controllers
{
    using PaymentsBudgetSystem.Controllers;
    using Core.Contracts;
    using Core.Models.Assets;
    using Core.Models.Enums;

    using static Common.ExceptionMessages.Asset;

    [TestFixture]
    internal class AssetsControllerTests : ControllerTestBase
    {
        private AssetsController assetsController;

        private Mock<IAssetService> mockAssetService;

        private Guid testAssetId;

        [SetUp]
        public void Setup()
        {
            testAssetId = Guid.NewGuid();

            mockAssetService = new Mock<IAssetService>();

            AllAssetsViewModel allAssetsTestModel = GetDefaultAllAssetsViewModel();
            
            mockAssetService
                .Setup(s => s.GetAllAssetsAsync(It.IsAny<string>(), It.IsAny<AllAssetsViewModel>()))
                .ReturnsAsync(allAssetsTestModel);

            int testYear = 2023;
            mockAssetService
                .Setup(s => s.GetAssetDetailsAsync(testUserId, testAssetId, testYear))
                .ReturnsAsync(new AssetDetailsViewModel());

            assetsController = new AssetsController(mockAssetService.Object)
            {
                ControllerContext = testControllerContext
            };

            assetsController.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Info_OnGet_ReturnsViewWithCorrectModel()
        {
            var expectedModel = GetDefaultAllAssetsViewModel();

            int month = expectedModel.InfoMonth;
            int year = expectedModel.InfoYear;
            string? name = expectedModel.NameFilter;
            int attribute = (int)expectedModel.SortAttribute;
            int sortBy = (int)expectedModel.SortBy;
            int page = expectedModel.Page;

            var result = await assetsController.Info(year, month, name, attribute, sortBy, page);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public void Info_OnPost_RedirectsWithProperArguments()
        {
            var testModel = GetDefaultAllAssetsViewModel();

            var result = assetsController.Info(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);

            List<string> actualRouteValues = redirectResult.RouteValues.Select(v => v.Value.ToString()).ToList();

            Assert.That(actualRouteValues.Count, Is.EqualTo(6));
            Assert.That(redirectResult.ActionName, Is.EqualTo("Info"));
            Assert.That(actualRouteValues[0], Is.EqualTo(testModel.InfoYear.ToString()));
            Assert.That(actualRouteValues[1], Is.EqualTo(testModel.InfoMonth.ToString()));
            Assert.That(actualRouteValues[2], Is.EqualTo(testModel.NameFilter));
            Assert.That(actualRouteValues[3], Is.EqualTo(((int)testModel.SortAttribute).ToString()));
            Assert.That(actualRouteValues[4], Is.EqualTo(((int)testModel.SortBy).ToString()));
            Assert.That(actualRouteValues[5], Is.EqualTo(testModel.Page.ToString()));
        }

        [Test]
        public async Task Details_OnGet_ReturnsViewWithCorrectModel()
        {
            var testYear = 2023;
            var result = await assetsController.Details(testAssetId, testYear);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, new AssetDetailsViewModel());
        }

        [Test]
        public async Task Details_OnGet_RedirectsToErrorInCaseOfInvalidAssetId()
        {
            var invalidAssetId = Guid.NewGuid();
            var testYear = 2023;

            mockAssetService
                .Setup(s => s.GetAssetDetailsAsync(testUserId, invalidAssetId, testYear))
                .ThrowsAsync(new InvalidOperationException(InvalidAsset));

            var result = await assetsController.Details(invalidAssetId, testYear);
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult, InvalidAsset);
        }

        [Test]
        public async Task Details_OnPost_RedirectsToDetailsOnGetWithProperRouteValuesIfModelStateIsValid()
        {
            var testYear = 2023;

            var testModel = new AssetDetailsViewModel
            {
                AssetId = testAssetId,
                Year = testYear,
            };

            var result = await assetsController.Details(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Details"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.First().Value, Is.EqualTo(testAssetId));
            Assert.That(redirectResult.RouteValues.Last().Value, Is.EqualTo(testYear));
        }

        [Test]
        public async Task Details_OnPost_ReturnsViewWithProperModelIfModelStateIsInvalid()
        {
            var testYear = 2023;

            var testModel = new AssetDetailsViewModel
            {
                AssetId = testAssetId,
                Year = testYear,
            };

            assetsController.ModelState.AddModelError("", "");
            
            var result = await assetsController.Details(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, new AssetDetailsViewModel());
        }

        private AllAssetsViewModel GetDefaultAllAssetsViewModel()
            => new AllAssetsViewModel
            {
                InfoMonth = 1,
                InfoYear = 2023,
                NameFilter = "",
                SortAttribute = AssetSort.BalanceValue,
                SortBy = SortBy.Ascending,
                Page = 1
            };
    }
}
