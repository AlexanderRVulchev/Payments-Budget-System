namespace PaymentsBudgetSystem.Core.Models.Salaries
{
    public class EmployeeSalaryPaymentViewModel
    {
        public Guid? Id { get; set; }

        public Guid? PaymentId { get; set; }

        public Guid EmployeeId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public DateTime DateEmployed { get; set; }

        public DateTime? DateLeft { get; set; }

        public decimal NetSalaryJobContract { get; set; }

        public decimal NetSalaryStateOfficial { get; set; }

        public decimal InsurancePensionEmployer { get; set; }

        public decimal InsurancePensionEmployee { get; set; }

        public decimal InsuranceHealthEmployer { get; set; }

        public decimal InsuranceHealthEmployee { get; set; }

        public decimal InsuranceAdditionalEmployer { get; set; }

        public decimal InsuranceAdditionalEmployee { get; set; }

        public decimal IncomeTax { get; set; }
    }
}
