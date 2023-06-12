using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    using Enums;

    using static Common.DataConstants.Employee;

    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(EgnFixedLength)]
        public string Egn { get; set; } = null!;

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal MonthlySalary { get; set; }

        public DateTime DateEmployed { get; set; }

        public DateTime? DateLeft { get; set; }

        public ContractType ContractType { get; set; }

        public List<PaymentSalaryDetails> SalaryDetails { get; set; } = null!;
    }
}