using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class GlobalSetting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string SettingName { get; set; } = null!;

        [Column(TypeName = "DECIMAL(12, 4)")]
        public decimal SettingValue { get; set; }
    }
}
