using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Beneficiaries
{
    using static Common.DataConstants.Beneficiary;
    using static Common.ValidationErrors.General;
    using static Common.ValidationErrors.Beneficiary;

    public class BeneficiaryFormModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [StringLength(BeneficiaryNameMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Име на контрагента")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = FieldIsRequired)]
        [StringLength(BeneficiaryIdentifierFixedLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Булстат")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = BeneficiaryIdentifierMustBeNineDigits)]
        public string Identifier { get; set; } = null!;

        [StringLength(BeneficiaryAddressMaxLength, ErrorMessage = StringMaxLengthValidationError)]
        [Display(Name = "Адрес")]
        public string? Address { get; set; }

        [Required(ErrorMessage = FieldIsRequired)]
        [Display(Name = "Банкова сметка")]
        [RegularExpression(BankAccountRegex, ErrorMessage = BeneficiaryInvalidBankAccount)]
        public string BankAccount { get; set; } = null!;
    }
}
