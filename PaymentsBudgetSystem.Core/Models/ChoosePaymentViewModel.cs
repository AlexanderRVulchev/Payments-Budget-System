
namespace PaymentsBudgetSystem.Core.Models
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class ChoosePaymentViewModel
    {
        public List<BeneficiaryViewModel> Beneficiaries { get; set; } = new();

        public ParagraphType SelectedParagraph { get; set; }

        public Guid SelectedBeneficiary { get; set; }

        [Range(1, 12, ErrorMessage = InvalidMonthError)]
        public int SalaryMonth { get; set; }

        [Range(YearMinValue, YearMaxValue, ErrorMessage = InvalidYearError)]
        public int SalaryYear { get; set; }
    }
}
