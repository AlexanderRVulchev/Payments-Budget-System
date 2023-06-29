namespace PaymentsBudgetSystem.Core.Models.Cash
{
    using Data.Entities.Enums;

    public class CashPaymentViewModel
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public int CashOrderNumber { get; set; }

        public DateOnly CashOrderDate { get; set; }

        public decimal Amount { get; set; }

        public ParagraphType Type { get; set; }

        public DateOnly Date { get; set; }

        public string? Description { get; set; }

    }
}
