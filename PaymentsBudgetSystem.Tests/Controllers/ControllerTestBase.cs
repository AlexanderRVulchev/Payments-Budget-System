using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Security.Claims;

namespace PaymentsBudgetSystem.Tests.Controllers
{
    using Core.Models.Assets;
    using Core.Models.Beneficiaries;
    using Core.Models.Cash;
    using Core.Models.Employees;
    using Core.Models.Enums;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Core.Models;
    using PaymentsBudgetSystem.Core.Models.Information;

    [TestFixture]
    internal abstract class ControllerTestBase
    {
        protected Mock<ClaimsPrincipal> userMock;
        protected ControllerContext testControllerContext;
        protected string testUserId = "test user id";

        protected Guid testGuidId = Guid.NewGuid();

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

        protected NewAssetFormModel GetDefaultTestNewAssetFormModel()
            => new NewAssetFormModel
            {
                Beneficiary = new BeneficiaryFormModel
                {
                    Id = testGuidId
                },
                Position1SingleAssetValue = 100,
                Position1Name = "asset name",
                Position1Quantity = 1,
                BeneficiaryId = testGuidId,
                ParagraphType = ParagraphType.UpkeepLongTermAssets5100,
            };

        protected BeneficiaryFormModel GetDefaultTestBeneficiaryFormModel()
            => new BeneficiaryFormModel
            {
                Id = testGuidId,
                Address = "",
                BankAccount = "",
                Identifier = "",
                Name = ""
            };

        protected AllAssetsViewModel GetDefaultAllAssetsViewModel()
            => new AllAssetsViewModel
            {
                InfoMonth = 1,
                InfoYear = 2023,
                NameFilter = "",
                SortAttribute = AssetSort.BalanceValue,
                SortBy = SortBy.Ascending,
                Page = 1
            };

        protected AllBeneficiariesViewModel GetDefaultAllBeneficiariesViewModel()
            => new AllBeneficiariesViewModel
            {
                AddressFilter = "",
                BankAccountFilter = "",
                IdentifierFilter = "",
                NameFilter = "",
                Page = 1
            };

        protected BeneficiaryFormModel GetDefaultBeneficiaryFormModel()
            => new BeneficiaryFormModel
            {
                Address = "",
                BankAccount = "",
                Identifier = "",
                Name = ""
            };

        protected List<EmployeeListModel> GetDefaultListOfEmployeeListModel()
            => new List<EmployeeListModel>()
            {
                        new EmployeeListModel
                        {
                            EmployeeId = testGuidId,
                            EmployeeName = ""
                        }
            };

        protected CashPaymentViewModel GetDefaultCashPaymentViewModel()
            => new CashPaymentViewModel
            {
                Amount = 10,
                Employees = GetDefaultListOfEmployeeListModel()
            };

        protected CashPaymentDetailsModel GetDefaultCashPaymentDetailsModel()
            => new CashPaymentDetailsModel
            {
                Amount = 10,
                CashOrderNumber = 1,
                Description = "",
                Employee = new EmployeeListModel
                {
                    EmployeeId = testGuidId,
                    EmployeeName = " "
                },
                Type = ParagraphType.Materials1015,
                Id = testGuidId,
            };

        protected AllEmployeesViewModel GetDefaultAllEmployeesViewModel()
            => new AllEmployeesViewModel
            {
                SortAttribute = EmployeeSort.TotalIncome,
                SortBy = SortBy.Descending,
                Egn = "",
                FirstName = "",
                LastName = "",
                Page = 1
            };

        protected EmployeeFormModel GetDefaultEmployeeFormModel()
            => new EmployeeFormModel
            {
                ContractType = ContractType.JobContract,
                MonthlySalary = 4000,
                DateEmployed = new DateTime(2023, 1, 1),
                Egn = "",
                DateLeft = null,
                FirstName = "",
                LastName = ""
            };

        protected BeneficiaryViewModel GetDefaultBeneficiaryViewModel()
            => new BeneficiaryViewModel
            {
                Address = "",
                BankAccount = "",
                Identifier = "",
                Name = "",
                BeneficiaryId = testGuidId
            };

        protected List<BeneficiaryViewModel> GetDefaultListOfBeneficiaryViewModel()
            => new List<BeneficiaryViewModel>
            {
                GetDefaultBeneficiaryViewModel()
            };

        protected ChoosePaymentViewModel GetDefaultChoosePaymentViewModel()
            => new ChoosePaymentViewModel
            {
                Beneficiaries = GetDefaultListOfBeneficiaryViewModel()
            };

        protected PaymentInformationViewModel GetDefaultPaymentInformationViewModel()
            => new PaymentInformationViewModel
            {
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 12, 31),
                SortBy = SortBy.Descending,
                InformationSort = InformationSort.Amount,
                AmountMin = 1,
                AmountMax = 10000,
                Page = 1,
                ReceiverNameFilter = ""
            };
    }
}
