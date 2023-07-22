using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Budget
{
    using static Common.DataConstants.General;

    public class IndividualBudgetFormData
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public int FiscalYear { get; set; }

        [Range(0, MoneyMaxValue)]
        public decimal SalariesLimit { get; set; }

        public decimal SalariesExpenses { get; set; }

        [Range(0, MoneyMaxValue)]
        public decimal SupportLimit { get; set; }

        public decimal SupportExpenses { get; set; }

        [Range(0, MoneyMaxValue)]
        public decimal AssetsLimit { get; set; }

        public decimal AssetsExpenses { get; set; }
    }
}
