
namespace PaymentsBudgetSystem.Core.Models.Assets
{
    using Core.Models.Beneficiaries;
    using PaymentsBudgetSystem.Data.Entities.Enums;

    public class AssetPaymentDetailsViewModel
    {
        public BeneficiaryViewModel Beneficiary { get; set; } = null!;

        public Guid Id { get; set; }

        public string? InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public decimal Amount { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public List<AssetShortViewModel> Assets { get; set; } = new();
    }
}
