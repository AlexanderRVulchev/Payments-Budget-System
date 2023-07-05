
namespace PaymentsBudgetSystem.Core.Models.Cash
{
    using Core.Models.Employees;
    using Data.Entities.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Common.ValidationErrors.General;

    public class CashPaymentDetailsModel
    {
        public Guid Id { get; set; }

        public int CashOrderNumber { get; set; }

        public decimal Amount { get; set; }

        public ParagraphType Type { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public EmployeeListModel Employee { get; set; } = new();
    }
}
