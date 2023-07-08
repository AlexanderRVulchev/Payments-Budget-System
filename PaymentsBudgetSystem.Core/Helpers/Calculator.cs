namespace PaymentsBudgetSystem.Core.Helpers
{
    using Models;
    using Models.Assets;
    using Models.Enums;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Core.Models.Salaries;
    using PaymentsBudgetSystem.Core.Models.Report;
    using PaymentsBudgetSystem.Core.Models.Information;

    public class Calculator
    {
        public AssetInfoViewModel CalculateAssetDataByYearAndMonth(
            int year,
            int month,
            AssetInfoViewModel asset,
            List<GlobalSettingDataModel> settings)
        {
            int numberOfMonthsSinceAquisition = (year - asset.DateAquired.Year) * 12 + (month - asset.DateAquired.Month);
            int totalLifeOfAssetInMonths = default;
            decimal residualValuePart = default;

            if (asset.Type == ParagraphType.UpkeepLongTermAssets5100)
            {
                residualValuePart = settings.First(s => s.Id == GlobalSetting.UpkeepResidualPart).SettingValue;
                totalLifeOfAssetInMonths = (int)settings.First(s => s.Id == GlobalSetting.UpkeepLife).SettingValue;
            }
            else if (asset.Type == ParagraphType.AquisitionLongTermAssets5200)
            {
                residualValuePart = settings.First(s => s.Id == GlobalSetting.TangibleAssetResidualPart).SettingValue;
                totalLifeOfAssetInMonths = (int)settings.First(s => s.Id == GlobalSetting.TangibleAssetLife).SettingValue;
            }
            else if (asset.Type == ParagraphType.AquisitionIntangibleAssets5300)
            {
                residualValuePart = settings.First(s => s.Id == GlobalSetting.IntangibleAssetResidualPart).SettingValue;
                totalLifeOfAssetInMonths = (int)settings.First(s => s.Id == GlobalSetting.IntangibleAssetLife).SettingValue;
            }

            decimal assetResidualValue = residualValuePart * asset.ReportValue;
            decimal totalAmortizationQuota = asset.ReportValue - assetResidualValue;
            decimal amortizationPerMonth = totalAmortizationQuota / totalLifeOfAssetInMonths;
            decimal assetAmortization = amortizationPerMonth * numberOfMonthsSinceAquisition;
            decimal assetBalanceValue = asset.ReportValue - assetAmortization;
            decimal amortizationQuotaLeft = totalAmortizationQuota - assetAmortization;

            if (assetAmortization > totalAmortizationQuota)
            {
                assetAmortization = asset.ReportValue - assetResidualValue;
                assetBalanceValue = asset.ReportValue - assetAmortization;
                amortizationQuotaLeft = 0;
            }

            asset.ResidualValue = assetResidualValue;
            asset.BalanceValue = assetBalanceValue;
            asset.AmortizationValue = assetAmortization;
            asset.AmortizationQuotaLeft = amortizationQuotaLeft;

            return asset;
        }

        public void CalculateEmployeeMonthlySalary(
            EmployeeSalaryPaymentViewModel employee,
            DateTime fromDay,
            DateTime toDay,
            List<GlobalSettingDataModel> settings)
        {
            int totalDays = (toDay - fromDay).Days + 1;

            if (employee.DateEmployed > fromDay)
            {
                fromDay = employee.DateEmployed;
            }
            if (employee.DateLeft != null && employee.DateLeft < toDay)
            {
                toDay = (DateTime)employee.DateLeft;
            }

            int employeeWorkDays = (toDay - fromDay).Days + 1;

            decimal grossSalary = employee.MonthlySalary * ((decimal)employeeWorkDays / (decimal)totalDays);

            if (employee.ContractType == ContractType.JobContract)
            {
                employee.InsurancePensionEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.InsurancePensionEmployer).SettingValue;
                employee.InsuranceAdditionalEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.AdditionalInsuranceEmployer).SettingValue;
                employee.InsuranceHealthEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.HealthInsuranceEmployer).SettingValue;

                employee.InsurancePensionEmployee = grossSalary * settings.First(s => s.Id == GlobalSetting.InsurancePensionEmployee).SettingValue;
                employee.InsuranceHealthEmployee = grossSalary * settings.First(s => s.Id == GlobalSetting.HealthInsuranceEmployee).SettingValue;
                employee.InsuranceAdditionalEmployee = grossSalary * settings.First(s => s.Id == GlobalSetting.AdditionalInsuranceEmployee).SettingValue;

                decimal socialSecurityDeductions = employee.InsurancePensionEmployee + employee.InsuranceHealthEmployee + employee.InsuranceAdditionalEmployee;
                decimal salaryAfterSocialSecurityDeductions = grossSalary - socialSecurityDeductions;

                decimal incomeTax = salaryAfterSocialSecurityDeductions * settings.First(s => s.Id == GlobalSetting.TaxRate).SettingValue;
                decimal netSalary = salaryAfterSocialSecurityDeductions - incomeTax;

                employee.IncomeTax = incomeTax;
                employee.NetSalaryJobContract = netSalary;
            }
            else if (employee.ContractType == ContractType.StateOfficial)
            {
                employee.InsurancePensionEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.InsurancePensionEmployer).SettingValue;
                employee.InsuranceHealthEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.HealthInsuranceEmployer).SettingValue;
                employee.InsuranceAdditionalEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.AdditionalInsuranceEmployer).SettingValue;

                employee.InsurancePensionEmployer += grossSalary * settings.First(s => s.Id == GlobalSetting.InsurancePensionEmployee).SettingValue;
                employee.InsuranceHealthEmployer += grossSalary * settings.First(s => s.Id == GlobalSetting.HealthInsuranceEmployee).SettingValue;
                employee.InsuranceAdditionalEmployer += grossSalary * settings.First(s => s.Id == GlobalSetting.AdditionalInsuranceEmployee).SettingValue;

                decimal incomeTax = grossSalary * settings.First(s => s.Id == GlobalSetting.TaxRate).SettingValue;
                decimal netSalary = grossSalary - incomeTax;

                employee.IncomeTax = incomeTax;
                employee.NetSalaryStateOfficial = netSalary;
            }
        }

        public void CalculateTotalPayroll(SalariesPaymentViewModel model)
        {
            model.TotalInsurancePensionEmployer = model.IndividualSalaries.Sum(s => s.InsurancePensionEmployer);
            model.TotalInsurancePensionEmployee = model.IndividualSalaries.Sum(s => s.InsurancePensionEmployee);
            model.TotalInsuranceHealthEmployer = model.IndividualSalaries.Sum(s => s.InsuranceHealthEmployer);
            model.TotalInsuranceHealthEmployee = model.IndividualSalaries.Sum(s => s.InsuranceHealthEmployee);
            model.TotalInsuranceAdditionalEmployer = model.IndividualSalaries.Sum(s => s.InsuranceHealthEmployer);
            model.TotalInsuranceAdditionalEmployee = model.IndividualSalaries.Sum(s => s.InsuranceHealthEmployee);
            model.TotalIncomeTax = model.IndividualSalaries.Sum(s => s.IncomeTax);
            model.TotalNetSalaryJobContract = model.IndividualSalaries.Sum(s => s.NetSalaryJobContract);
            model.TotalNetSalaryStateOfficial = model.IndividualSalaries.Sum(s => s.NetSalaryStateOfficial);

            model.Amount = model.TotalInsurancePensionEmployer
                + model.TotalInsurancePensionEmployee
                + model.TotalInsuranceHealthEmployer
                + model.TotalInsuranceHealthEmployee
                + model.TotalInsuranceAdditionalEmployer
                + model.TotalInsuranceAdditionalEmployee
                + model.TotalIncomeTax
                + model.TotalNetSalaryJobContract
                + model.TotalNetSalaryStateOfficial;
        }

        public void CalculateReportExpenses(
            ReportDataModel model,
            List<ReportExpensesDataModel>? payments)
        {
            if (payments == null)
            {
                return;
            }

            model.Bank0101 = payments.Where(p => p.PaymentType == PaymentType.Salaries).Sum(p => p.NetSalaryJobContract);
            model.Bank0102 = payments.Where(p => p.PaymentType == PaymentType.Salaries).Sum(p => p.NetSalaryStateOfficial);

            model.Transfer0551 = payments.Where(p => p.PaymentType == PaymentType.Salaries).Sum(p => p.InsurancePension);
            model.Transfer0560 = payments.Where(p => p.PaymentType == PaymentType.Salaries).Sum(p => p.InsuranceHealth);
            model.Transfer0580 = payments.Where(p => p.PaymentType == PaymentType.Salaries).Sum(p => p.InsuranceAdditional);
            model.Transfer0590 = payments.Where(p => p.PaymentType == PaymentType.Salaries).Sum(p => p.IncomeTax);

            model.Bank1015 = payments.Where(p => p.PaymentType == PaymentType.Support && p.ParagraphType == ParagraphType.Materials1015).Sum(p => p.Amount);
            model.Bank1020 = payments.Where(p => p.PaymentType == PaymentType.Support && p.ParagraphType == ParagraphType.Services1020).Sum(p => p.Amount);
            model.Bank1051 = payments.Where(p => p.PaymentType == PaymentType.Support && p.ParagraphType == ParagraphType.BusinessTrips1051).Sum(p => p.Amount);

            model.Cash1015 = payments.Where(p => p.PaymentType == PaymentType.Cash && p.ParagraphType == ParagraphType.Materials1015).Sum(p => p.Amount);
            model.Cash1020 = payments.Where(p => p.PaymentType == PaymentType.Cash && p.ParagraphType == ParagraphType.Services1020).Sum(p => p.Amount);
            model.Cash1051 = payments.Where(p => p.PaymentType == PaymentType.Cash && p.ParagraphType == ParagraphType.BusinessTrips1051).Sum(p => p.Amount);

            model.Bank5100 = payments.Where(p => p.PaymentType == PaymentType.Assets && p.ParagraphType == ParagraphType.UpkeepLongTermAssets5100).Sum(p => p.Amount);
            model.Bank5200 = payments.Where(p => p.PaymentType == PaymentType.Assets && p.ParagraphType == ParagraphType.AquisitionLongTermAssets5200).Sum(p => p.Amount);
            model.Bank5300 = payments.Where(p => p.PaymentType == PaymentType.Assets && p.ParagraphType == ParagraphType.AquisitionIntangibleAssets5300).Sum(p => p.Amount);
        }

        public decimal CalculateFreeSupportFunds(ReportDataModel report)
        {
            decimal totalSupportExpenses =
                  report.Cash1015
                + report.Bank1015
                + report.Bank1020
                + report.Cash1020
                + report.Cash1051
                + report.Bank0101;

            return report.SupportLimit - totalSupportExpenses;
        }

        public decimal CalculateFreeSalariesFunds(ReportDataModel report)
        {
            decimal totalSalariesExpenses = 
                  report.Bank0101
                + report.Bank0102
                + report.Transfer0551
                + report.Transfer0560
                + report.Transfer0580
                + report.Transfer0590;

            return report.SalariesLimit - totalSalariesExpenses;
        }

        public decimal CalculateFreeAssetsFunds(ReportDataModel report)
        {
            decimal totalAssetsExpenses =
                  report.Bank5100
                + report.Bank5200
                + report.Bank5300;

            return report.AssetsLimit - totalAssetsExpenses;
        }
    }
}
