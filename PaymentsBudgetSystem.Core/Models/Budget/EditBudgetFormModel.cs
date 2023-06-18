namespace PaymentsBudgetSystem.Core.Models.Budget
{
    public class EditBudgetFormModel
    {
        public ConsolidatedBudgetViewModel ConsolidatedBudget { get; set; } = null!;

        public List<IndividualBudgetFormData> IndividualBudgetsData { get; set; } = new();

        public int FiscalYear { get; set; }

        public Guid Id { get; set; }

        public decimal NewSalaryLimit { get; set; }

        public decimal NewSupportLimit { get; set; }

        public decimal NewAssetsLimit { get; set; }
    }
}
