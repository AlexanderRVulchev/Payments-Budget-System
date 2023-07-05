using Microsoft.VisualBasic;
using PaymentsBudgetSystem.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Employees
{
    using static Common.DataConstants.Employee;
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class EmployeeFormModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = StringLengthValidationError)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength, ErrorMessage = StringLengthValidationError)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [StringLength(EgnFixedLength, MinimumLength = EgnFixedLength, ErrorMessage = StringFixedLength)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = EgnRegexValidationMessage)]
        [Display(Name = "ЕГН")]
        public string Egn { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [Display(Name = "Брутна заплата")]
        [Range(typeof(decimal), DecimalMoneyMinValue, DecimalMoneyMaxValue, ErrorMessage = MoneyValidationError)]
        public decimal MonthlySalary { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [Display(Name = "Дата на назначаване")]
        public DateTime DateEmployed { get; set; }
                
        public DateTime? DateLeft { get; set; }

        public ContractType ContractType { get; set; }

        public string DateLeftToString => this.DateLeft?.ToShortDateString() ?? string.Empty;
    }
}
