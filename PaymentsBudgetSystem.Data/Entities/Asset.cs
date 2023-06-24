using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using Enums;

    using static Common.DataConstants.Asset;

    public class Asset
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public DateTime DateAquired { get; set; }

        public DateTime? DateDisposed { get; set; }

        [Required]
        public ParagraphType Type { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal ReportValue { get; set; }

        [Required]
        [MaxLength(AssetDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(PaymentAssetsDetails))]
        public Guid PaymentAssetDetailsId { get; set; }
        public PaymentAssetsDetails PaymentAssetsDetails { get; set; } = null!;
    }
}
