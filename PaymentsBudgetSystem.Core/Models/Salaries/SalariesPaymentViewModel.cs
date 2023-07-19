using PaymentsBudgetSystem.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Salaries
{
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class SalariesPaymentViewModel
    {
        [Required(ErrorMessage = FieldIsRequired)]
        [Range(YearMinValue, YearMaxValue, ErrorMessage = InvalidYearError)]
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
