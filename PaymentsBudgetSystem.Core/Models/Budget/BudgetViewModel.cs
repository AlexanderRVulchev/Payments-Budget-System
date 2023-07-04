namespace PaymentsBudgetSystem.Core.Models.Budget
{
    public class BudgetViewModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int FiscalYear { get; set; }

        public decimal SalariesLimit { get; set; }

        public decimal SalariesExpenses { get; set; }

        public decimal SupportLimit { get; set; }

        public decimal SupportExpenses { get; set; }

        public decimal AssetsLimit { get; set; }

        public decimal AssetsExpenses { get; set; }
    }
}
