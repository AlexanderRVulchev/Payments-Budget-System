using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using Enums;

    public class PaymentSupportDetails
    {
        [Key]
        [Required]
        [ForeignKey(nameof(Payment))]
        public Guid SupportPaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Beneficiary))]
        public Guid BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; } = null!;

        public string? InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }
    }
}
