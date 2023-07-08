using PaymentsBudgetSystem.Core.Models.Beneficiaries;
using PaymentsBudgetSystem.Data.Entities.Enums;

namespace PaymentsBudgetSystem.Core.Models.Support
{
    public class SupportPaymentDetailsViewModel
    {
        public BeneficiaryViewModel Beneficiary { get; set; } = null!;

        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public PaymentType PaymentType { get; set; }

        public DateTime Date { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public string? Description { get; set; }

        public string? InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }
    }
}
