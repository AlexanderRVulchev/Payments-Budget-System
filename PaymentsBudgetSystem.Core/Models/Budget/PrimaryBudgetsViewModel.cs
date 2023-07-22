using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Budget
{
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class PrimaryBudgetsViewModel
    {
        public List<BudgetViewModel> IndividualBudgets { get; set; } = new();

        public List<ConsolidatedBudgetViewModel> ConsolidatedBudgets { get; set; } = new();

        [Range(1990, 2100, ErrorMessage = InvalidYearError)]
        public int NewBudgetYear { get; set; }

        [Range(DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Средства")]
        public decimal NewBudgetFunds { get; set; }
    }
}
