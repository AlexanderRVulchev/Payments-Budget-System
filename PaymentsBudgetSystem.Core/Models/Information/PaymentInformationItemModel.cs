using PaymentsBudgetSystem.Data.Entities.Enums;

namespace PaymentsBudgetSystem.Core.Models.Information
{
    public class PaymentInformationItemModel
    {
        public Guid PaymentId { get; set; }

        public decimal Amount { get; set; }

        public PaymentType PaymentType { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public string? Description { get; set; }

        public string ReceiverName { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}
