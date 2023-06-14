namespace PaymentsBudgetSystem.Core.Models.Budget
{
    public class PrimaryBudgetsViewModel
    {
        public List<BudgetViewModel> IndividualBudgets { get; set; } = new();

        public List<BudgetViewModel> ConsolidatedBudgets { get; set; } = new();
    }
}
