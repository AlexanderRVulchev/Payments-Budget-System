namespace PaymentsBudgetSystem.Tests.Helpers
{
    using Core.Helpers;
    using Core.Models;
    using Core.Models.Assets;
    using Core.Models.Enums;
    using Core.Models.Report;
    using Core.Models.Salaries;
    using Data.Entities.Enums;

    using static Tests.GlobalSettingsTestSeeder;

    [TestFixture]
    internal class CalculatorTests
    {
        private Calculator calculator;
        private List<GlobalSettingDataModel> globalSettings;
        private AssetInfoViewModel testAssetModel;
        private EmployeeSalaryPaymentViewModel testEmployeeModel;

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator();

            globalSettings = GetGlobalSettings()
           .Select(gs => new GlobalSettingDataModel
           {
               Id = (GlobalSetting)gs.Id,
               SettingName = gs.SettingName,
               SettingValue = gs.SettingValue
           })
           .ToList();

            testAssetModel = new AssetInfoViewModel
            {
                ReportValue = 100,
                DateAquired = new DateTime(2023, 1, 1),
                Name = "",
                Type = ParagraphType.AquisitionLongTermAssets5200,
                AssetId = Guid.NewGuid(),
                PaymentId = Guid.NewGuid(),
                TypeText = ""
            };

            testEmployeeModel = new EmployeeSalaryPaymentViewModel
            {
                MonthlySalary = 4500,
                DateEmployed = new DateTime(2023, 1, 2),
                DateLeft = new DateTime(2023, 1, 23),
                ContractType = ContractType.JobContract,
                EmployeeId = Guid.NewGuid(),
                EmployeeName = ""
            };
        }

        [Test]
        public void CalculateAssetDataByYearAndMonth_ReturnsProperAssetData()
        {
            int testYear = 2023;
            int testMonth = 1;

            var result = calculator.CalculateAssetDataByYearAndMonth(testYear, testMonth, testAssetModel, globalSettings);

            Assert.IsNotNull(result);
            Assert.That(result.AmortizationQuotaLeft == 85);
            Assert.That(result.AmortizationValue == 0);
            Assert.That(result.BalanceValue == 100);
            Assert.That(result.ResidualValue == 15);
        }

        [Test]
        public void CalculateAssetDataByYearAndMonth_DoesNotAllowForNegativeAmortizationQuota()
        {
            int testYear = 2035;
            int testMonth = 5;

            var result = calculator.CalculateAssetDataByYearAndMonth(testYear, testMonth, testAssetModel, globalSettings);

            Assert.IsNotNull(result);
            Assert.That(result.AmortizationQuotaLeft, Is.EqualTo(0));
        }

        [Test]
        public void CalculateEmployeeMonthlySalary_ReturnsProperSalaryDataForJobContract() 
        {
            var fromDay = new DateTime(2023, 1, 4);
            var toDay = new DateTime(2023, 1, 23);

            calculator.CalculateEmployeeMonthlySalary(testEmployeeModel, fromDay, toDay, globalSettings);

            Assert.IsNotNull(testEmployeeModel);
            Assert.That(testEmployeeModel.NetSalaryJobContract == 3402.81m);
        }

        [Test]
        public void CalculateEmployeeMonthlySalary_SetsStartDateAndEndDateCorrectlyWhenInsideOfTheCurrentMonthAndCalculatesStateOfficialSalaryProperly()
        {
            testEmployeeModel.ContractType = ContractType.StateOfficial;

            var fromDay = new DateTime(2023, 1, 1);
            var toDay = new DateTime(2023, 1, 31);

            calculator.CalculateEmployeeMonthlySalary(testEmployeeModel, fromDay, toDay, globalSettings);

            Assert.IsNotNull(testEmployeeModel);
            Assert.That(testEmployeeModel.NetSalaryStateOfficial > 2874.19m);
            Assert.That(testEmployeeModel.NetSalaryStateOfficial < 2874.20m);
        }

        [Test]
        public void CalculateReportExpenses_ReturnsCorrectReportData()
        {
            var testReportModel = new ReportDataModel();

            var testPaymentsModel = new List<ReportExpensesDataModel>()
            {
                new ReportExpensesDataModel
                {
                    IncomeTax = 100,
                    PaymentType = PaymentType.Salaries,
                    ParagraphType = ParagraphType.IncomeTax0590,                    
                },
            };

            calculator.CalculateReportExpenses(testReportModel, testPaymentsModel);

            Assert.That(testReportModel.Transfer0590, Is.EqualTo(100));
        }

        [Test]
        public void CalculateReportExpenses_ReturnsZeroReportWhenPaymentsAreNull()
        {
            var testReportModel = new ReportDataModel();

            calculator.CalculateReportExpenses(testReportModel, null);

            Assert.That(testReportModel.Transfer0590, Is.EqualTo(0));
            Assert.That(testReportModel.Cash1051, Is.EqualTo(0));
            Assert.That(testReportModel.Bank5300, Is.EqualTo(0));
        }

        [Test]
        public void CalculateFreeSupportFunds_ReturnsCorrectResult()
        {
            var testReportModel = new ReportDataModel
            {
                SupportLimit = 100,
                Bank1015 = 5,
                Bank1020 = 10,
                Cash1051 = 15
            };

            var result = calculator.CalculateFreeSupportFunds(testReportModel);

            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void CalculateFreeSalariesFunds_ReturnsCorrectResult()
        {
            var testReportModel = new ReportDataModel
            {
                SalariesLimit = 100,
                Bank0101 = 20,
                Bank0102 = 10                
            };

            var result = calculator.CalculateFreeSalariesFunds(testReportModel);

            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void CalculateFreeAssetsFunds_ReturnsCorrectResult()
        {
            var testReportModel = new ReportDataModel
            {
                AssetsLimit = 100,
                Bank5100 = 20,
                Bank5200 = 10
            };

            var result = calculator.CalculateFreeAssetsFunds(testReportModel);

            Assert.That(result, Is.EqualTo(70));
        }
    }
}
