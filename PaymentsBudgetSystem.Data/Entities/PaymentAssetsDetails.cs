using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class PaymentAssetsDetails
    {
        [Key]
        [Required]
        [ForeignKey(nameof(Payment))]
        public Guid AssetPaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Beneficiary))]
        public Guid BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; } = null!;

        public string? InvoiceNumber { get; set; } 

        public DateTime? InvoiceDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        public HashSet<Asset> Assets { get; set; } = new();
    }
}
