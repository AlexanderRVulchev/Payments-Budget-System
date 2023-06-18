namespace PaymentsBudgetSystem.Core.Models.Budget
{
    public class ConsolidatedBudgetViewModel
    {
        public Guid Id { get; set; }

        public string? UserId { get; set; } 

        public string? Name { get; set; } 

        public int FiscalYear { get; set; }

        public decimal TotalLimit { get; set; }

        public decimal Allocated { get; set; }

        public decimal Unallocated { get; set; }
    }
}
