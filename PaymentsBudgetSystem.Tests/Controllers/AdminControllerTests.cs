using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Areas.Administration.Controllers;
    using Core.Contracts;

    using static Common.ExceptionMessages.Report;

    [TestFixture]
    internal class AdminControllerTests : ControllerTestBase
    {
        private AdminController controller;

        private Mock<IAdminService> mockAdminService;

        [SetUp]
        public void Setup()
        {
            mockAdminService = new();

            mockAdminService
                .Setup(s => s.GetGlobalSettingsAsync())
                .ReturnsAsync(GetDefaultGlobalSettingsEditModel());

            mockAdminService
                .Setup(s => s.GetAllReportsAsync())
                .ReturnsAsync(GetDefaultDeleteReportFormModel());

            controller = new AdminController(mockAdminService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Settings_OnGet_ReturnsViewWithCorrectModel()
        {
            var expectedModel = GetDefaultGlobalSettingsEditModel();

            var result = await controller.Settings();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task Settings_OnPost_ReturnsViewWithCorrectModelIfModelStateIsInvalid()
        {
            controller.ModelState.AddModelError("", "");

            var testModel = GetDefaultGlobalSettingsEditModel();

            var result = await controller.Settings(testModel);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, testModel);
        }

        [Test]
        public async Task Settings_OnPost_RedirectsToSettingsIfSettingsAreSavedSuccessfully()
        {
            var testModel = GetDefaultGlobalSettingsEditModel();

            var result = await controller.Settings(testModel);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Settings"));
            Assert.That(controller.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.FirstOrDefault().Value, Is.EqualTo("Настойките са запазени успешно!"));
        }

        [Test]
        public async Task Reports_ReturnsViewWithCorrectModel()
        {
            var expectedModel = GetDefaultDeleteReportFormModel();

            var result = await controller.Reports();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public async Task DeleteReport_RedirectsToReportsIfDeletionIsSuccessful()
        {
            var result = await controller.DeleteReport(testGuidId);
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Reports"));
            Assert.That(controller.TempData.FirstOrDefault().Key, Is.EqualTo("SuccessMessage"));
            Assert.That(controller.TempData.FirstOrDefault().Value, Is.EqualTo("Отчетът е изтрит успешно!"));
        }

        [Test]
        public async Task DeleteReport_RedirectsToErrorIfReportIdIsInvalid()
        {
            mockAdminService
                .Setup(s => s.DeleteReportByIdAsync(testGuidId))
                .ThrowsAsync(new InvalidOperationException(ReportDoesNotExist));

            var result = await controller.DeleteReport(testGuidId);
            var redirectResult = result as RedirectToActionResult;

            AssertRedirectToError(redirectResult, ReportDoesNotExist);
        }
    }
}
