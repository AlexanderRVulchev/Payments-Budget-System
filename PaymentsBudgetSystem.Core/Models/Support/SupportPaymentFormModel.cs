
namespace PaymentsBudgetSystem.Core.Models.Support
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.Payment;

    public class SupportPaymentFormModel
    {
        public BeneficiaryFormModel Beneficiary { get; set; } = null!;

        public ParagraphType ParagraphType { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(InvoiceNumberMaxLength)]
        public string? InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }
    }
}
