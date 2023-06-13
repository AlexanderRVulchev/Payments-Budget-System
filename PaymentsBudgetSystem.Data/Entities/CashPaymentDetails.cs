using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsBudgetSystem.Data.Entities
{
    public class CashPaymentDetails
    {
        [Key]
        [Required]
        [ForeignKey(nameof(Payment))]
        public Guid CashPaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [Required]
        public int CashOrderNumber { get; set; }

        [Required]
        public DateTime CashOrderDate { get; set ; } = DateTime.UtcNow;
    }
}
