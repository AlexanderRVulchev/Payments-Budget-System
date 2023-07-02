using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int Year { get; set; }

        public int Month { get; set; }

        public bool IsConsolidated { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank0101 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank0102 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Transfer0551 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Transfer0560 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Transfer0580 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Transfer0590 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank1015 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Cash1015 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank1020 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Cash1020 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Cash1051 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank1051 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank5100 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank5200 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Bank5300 { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal LimitSalaries { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal LimitSupport { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal LimitAssets { get; set; } 
    }
}
