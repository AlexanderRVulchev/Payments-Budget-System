using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Security.Claims;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    [TestFixture]
    internal abstract class ControllerTestBase
    {
        protected Mock<ClaimsPrincipal> userMock;
        protected ControllerContext testControllerContext;
        protected string testUserId = "test user id";

        [SetUp]
        protected void BaseSetup()
        {
            userMock = new Mock<ClaimsPrincipal>();

            userMock.Setup(mock => mock
                .FindFirst(ClaimTypes.NameIdentifier))
                .Returns(new Claim(ClaimTypes.NameIdentifier, testUserId));

            testControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userMock.Object }
            };
        }

        protected void AssertRedirectToError(RedirectToActionResult? result, string message)
        {
            Assert.IsNotNull(result);
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
            Assert.That(result.ActionName, Is.EqualTo("Error"));
            Assert.That(result.RouteValues!.Last().Key, Is.EqualTo("errorMessage"));
            Assert.That(result.RouteValues!.Last().Value, Is.EqualTo(message));
        }

        protected void AssertObjectEquality(object? actual, object? expected)
        {
            Assert.IsNotNull(actual);
            Assert.IsNotNull(expected);

            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }
    }
}
