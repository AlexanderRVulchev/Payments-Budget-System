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

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue)]
        public decimal SalariesLimit { get; set; }

        public decimal SalariesExpenses { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue)]
        public decimal SupportLimit { get; set; }

        public decimal SupportExpenses { get; set; }

        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue)]
        public decimal AssetsLimit { get; set; }

        public decimal AssetsExpenses { get; set; }
    }
}
