using PaymentsBudgetSystem.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Beneficiaries
{
    public class AllBeneficiariesViewModel
    {
        public List<BeneficiaryViewModel> Beneficiaries { get; set; } = new();

        [Display(Name = "Име")]
        public string? NameFilter { get; set; }

        [Display(Name = "Булстат")]
        [StringLength(9, MinimumLength = 9)]
        public string? IdentifierFilter { get; set; }

        [Display(Name = "Адрес")]
        public string? AddressFilter { get; set; }

        public BeneficiarySort SortAttribute { get; set; }

        public SortBy SortBy { get; set; }
    }
}
