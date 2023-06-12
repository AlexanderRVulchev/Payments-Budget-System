using PaymentsBudgetSystem.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class CashPaymentDetails
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
    }
}
