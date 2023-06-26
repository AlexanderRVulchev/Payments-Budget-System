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
        public decimal InsurancePensionEmployer { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsurancePensionEmployee { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsuranceHealthEmployer { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsuranceHealthEmployee { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsuranceAdditionalEmployer { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal InsuranceAdditionalEmployee { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal IncomeTax { get; set; }
    }
}
