using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.User
{
    using static Common.DataConstants.User;
    using static Common.ValidationErrors.General;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength, ErrorMessage = StringLengthValidationError)]
        [Display(Name = "Потребителското име")]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = StringLengthValidationError)]
        [Display(Name = "Паролата")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = PasswordDoesntMatchError)]
        [Display(Name = "Паролата")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [Display(Name = "Името на институцията")]
        [StringLength (NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = StringLengthValidationError)]
        public string Name { get; set; } = null!;

        public List<string> PrimaryInstitutionName { get; set; } = new();

        public int InputForPrimary { get; set; }
    }
}

