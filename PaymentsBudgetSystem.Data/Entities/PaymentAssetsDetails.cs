using PaymentsBudgetSystem.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class PaymentAssetsDetails
    {
        public Guid Id { get; set; } = new();

        [Required]
        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Beneficiary))]
        public Guid BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; } = null!;

        public HashSet<Asset> Assets { get; set; } = new();
    }
}
