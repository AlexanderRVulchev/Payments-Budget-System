using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using static Common.DataConstants.Beneficiary;

    public class Beneficiary
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(BeneficiaryNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BeneficiaryIdentifierFixedLength)]
        public string Identifier { get; set; } = null!;

        [MaxLength(BeneficiaryAddressMaxLength)]
        public string? Address { get; set; }

        public HashSet<PaymentSupportDetails> SupportDetails { get; set; } = new();

        public HashSet<PaymentAssetsDetails> AssetsDetails { get; set; } = new();
    }
}
