using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class IndividualBudget
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int FiscalYear { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal SalariesLimit { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal SupportLimit { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal AssetsLimit { get; set; }
    }
}
