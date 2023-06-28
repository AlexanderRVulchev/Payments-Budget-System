namespace PaymentsBudgetSystem.Core.Helpers
{
    using Models;
    using Models.Assets;
    using Models.Enums;
    using Data.Entities.Enums;
    using PaymentsBudgetSystem.Core.Models.Salaries;

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

            if (assetAmortization > totalAmortizationQuota)
            {
                assetAmortization = asset.ReportValue - assetResidualValue;
                assetBalanceValue = asset.ReportValue - assetAmortization;
            }

            asset.ResidualValue = assetResidualValue;
            asset.BalanceValue = assetBalanceValue;
            asset.Amortization = assetAmortization;
            


            return asset;
        } 

        public void CalculateEmployeeMonthlySalary(
            EmployeeSalaryPaymentViewModel employee, 
            DateTime fromDay,
            DateTime toDay,
            List<GlobalSettingDataModel> settings)
        {
            int totalDays = (toDay - fromDay).Days;

            if (employee.DateEmployed > fromDay)
            {
                fromDay = employee.DateEmployed;
            }
            if (employee.DateLeft != null && employee.DateLeft < toDay)
            {
                toDay = (DateTime)employee.DateLeft;
            }

            int employeeWorkDays = (fromDay - toDay).Days;

            decimal grossSalary = employee.MonthlySalary * (employeeWorkDays / totalDays);

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

                employee.NetSalaryJobContract = netSalary;
            }
            else if (employee.ContractType == ContractType.StateOfficial)
            {
                employee.InsurancePensionEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.InsurancePensionEmployer).SettingValue;
                employee.InsuranceAdditionalEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.AdditionalInsuranceEmployer).SettingValue;
                employee.InsuranceHealthEmployer = grossSalary * settings.First(s => s.Id == GlobalSetting.HealthInsuranceEmployer).SettingValue;

                employee.InsurancePensionEmployer += grossSalary * settings.First(s => s.Id == GlobalSetting.InsurancePensionEmployee).SettingValue;
                employee.InsuranceHealthEmployer += grossSalary * settings.First(s => s.Id == GlobalSetting.HealthInsuranceEmployee).SettingValue;
                employee.InsuranceAdditionalEmployer += grossSalary * settings.First(s => s.Id == GlobalSetting.AdditionalInsuranceEmployee).SettingValue;

                decimal incomeTax = grossSalary * settings.First(s => s.Id == GlobalSetting.TaxRate).SettingValue;
                decimal netSalary = grossSalary - incomeTax;

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
    }
}
