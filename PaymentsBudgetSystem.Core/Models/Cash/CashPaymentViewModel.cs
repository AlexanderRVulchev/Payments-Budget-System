using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Cash
{
    using Data.Entities.Enums;
    using Core.Models.Employees;

    using static Common.ValidationErrors.General;
    using static Common.DataConstants.Payment;

    public class CashPaymentViewModel
    {
        public Guid Id { get; set; }

        public Guid SelectedEmployee { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = OrderNumberMustBeAPositiveNumber)]
        [Display(Name = "Номер на ордер")]
        public int CashOrderNumber { get; set; }

        [Required]
        [Range(typeof(decimal), AmountMinValue, AmountMinValue, ErrorMessage = PaymentMoneyCannotBeZeroOrLess)]
        [Display(Name = "Сума")]        
        public decimal Amount { get; set; }

        public ParagraphType Type { get; set; }

        public DateOnly Date { get; set; }

        [MaxLength(DescriptionMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        public List<EmployeeListModel> Employees { get; set; } = new();
    }
}
