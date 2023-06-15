namespace PaymentsBudgetSystem.Core.Models.Budget
{
    public class EditBudgetFormModel
    {
        public BudgetViewModel ConsolidatedBudget { get; set; } = null!;

        public List<IndividualBudgetFormData> IndividualBudgetsData { get; set; } = new();

        public decimal UnallocatedFunds { get; set; }
    }
}
