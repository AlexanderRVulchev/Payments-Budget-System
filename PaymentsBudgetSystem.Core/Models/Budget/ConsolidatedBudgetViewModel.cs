namespace PaymentsBudgetSystem.Core.Models.Budget
{
    public class ConsolidatedBudgetViewModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int FiscalYear { get; set; }

        public decimal TotalLimit { get; set; }

        public decimal Allocated { get; set; }

        public decimal Unallocated { get; set; }
    }
}
