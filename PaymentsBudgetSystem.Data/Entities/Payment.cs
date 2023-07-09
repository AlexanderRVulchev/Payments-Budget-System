using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using Enums;
    using static Common.DataConstants.Payment;

    public class Payment
    {
        [Key]
        public Guid Id { get; set; } = new();

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Amount { get; set; }

        public PaymentType PaymentType { get; set; }

        public DateTime Date { get; set; }

        public ParagraphType Paragraph { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [MaxLength(ReceiverNameMaxLength)]
        public string ReceiverName { get; set; } = null!;

        public PaymentSupportDetails SupportDetails { get; set; } = null!;

        public PaymentAssetsDetails AssetsDetails { get; set; } = null!;

        public List<PaymentSalaryDetails> SalariesDetails { get; set; } = new();

        public CashPaymentDetails CashDetails { get; set; } = null!;
    }
}
