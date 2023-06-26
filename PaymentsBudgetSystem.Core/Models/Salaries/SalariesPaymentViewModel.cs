namespace PaymentsBudgetSystem.Core.Models.Salaries
{
    public class SalariesPaymentViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal TotalNetSalaryJobContract { get; set; }

        public decimal TotalNetSalaryStateOfficial { get; set; }

        public decimal TotalInsurancePensionEmployer { get; set; }

        public decimal TotalInsurancePensionEmployee { get; set; }

        public decimal TotalInsuranceHealthEmployer { get; set; }

        public decimal TotalInsuranceHealthEmployee { get; set; }

        public decimal TotalInsuranceAdditionalEmployer { get; set; }

        public decimal TotalInsuranceAdditionalEmployee { get; set; }

        public decimal TotalIncomeTax { get; set; }

        public decimal Amount { get; set; }

        public List<EmployeeSalaryPaymentViewModel> IndividualSalaries { get; set; } = new();
    }
}
