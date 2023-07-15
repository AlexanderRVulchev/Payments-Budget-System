using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;


namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Contracts;
    using Core.Models;
    using Core.Models.Beneficiaries;
    using PaymentsBudgetSystem.Controllers;

    [TestFixture]
    internal class GatewayControllerTests : ControllerTestBase
    {
        private GatewayController controller;

        private Mock<IBeneficiaryService> mockBeneficiaryService;

        [SetUp]
        public void Setup()
        {
            mockBeneficiaryService = new();

            mockBeneficiaryService
                .Setup(s => s.GetAllBeneficiariesAsync(testUserId, It.IsAny<AllBeneficiariesViewModel>()))
                .ReturnsAsync(GetDefaultAllBeneficiariesViewModel());

            controller = new GatewayController(mockBeneficiaryService.Object)
            {
                ControllerContext = testControllerContext
            };

            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task ChoosePayment_ReturnsViewWithCorrectModel()
        {
            var expectedModel = new ChoosePaymentViewModel
            {
                Beneficiaries = GetDefaultAllBeneficiariesViewModel().Beneficiaries
            };

            var result = await controller.ChoosePayment();
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            AssertObjectEquality(viewResult.Model, expectedModel);
        }

        [Test]
        public void NewSupportPayment_RedirectsToChoosePaymentIfModelStateIsNotValid()
        {
            controller.ModelState.AddModelError("", "");

            var result = controller.NewSupportPayment(GetDefaultChoosePaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("ChoosePayment"));
        }

        [Test]
        public void NewSupportPayment_RedirectsToSupportPaymentIfModelStateIsValid()
        {
            var result = controller.NewSupportPayment(GetDefaultChoosePaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Payment"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("Support"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.Count, Is.EqualTo(2));
        }

        [Test]
        public void NewAssetPayment_RedirectsToChoosePaymentIfModelStateIsNotValid()
        {
            controller.ModelState.AddModelError("", "");

            var result = controller.NewAssetPayment(GetDefaultChoosePaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("ChoosePayment"));
        }

        [Test]
        public void NewAssetPayment_RedirectsToAssetPaymentIfModelStateIsValid()
        {
            var result = controller.NewAssetPayment(GetDefaultChoosePaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Payment"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("AssetPayment"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.Count, Is.EqualTo(2));
        }

        [Test]
        public void NewSalariesPayment_RedirectsToChoosePaymentIfModelStateIsNotValid()
        {
            controller.ModelState.AddModelError("", "");

            var result = controller.NewSalariesPayment(GetDefaultChoosePaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("ChoosePayment"));
        }

        [Test]
        public void NewSalariesPayment_RedirectsToAssetPaymentIfModelStateIsValid()
        {
            var result = controller.NewSalariesPayment(GetDefaultChoosePaymentViewModel());
            var redirectResult = result as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.That(redirectResult.ActionName, Is.EqualTo("Payment"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("Salaries"));
            Assert.IsNotNull(redirectResult.RouteValues);
            Assert.That(redirectResult.RouteValues.Count, Is.EqualTo(2));
        }

    }
}
