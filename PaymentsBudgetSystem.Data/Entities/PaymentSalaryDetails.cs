using PaymentsBudgetSystem.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class PaymentSalaryDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal NetSalaryJobContract { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal NetSalaryStateOfficial { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsurancePension { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsuranceHealth { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsuranceAdditional { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal IncomeTax { get; set; }
    }
}
