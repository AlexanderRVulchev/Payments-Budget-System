
namespace PaymentsBudgetSystem.Core.Models
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;

    public class ChoosePaymentViewModel
    {
        public List<BeneficiaryViewModel> Beneficiaries { get; set; } = new();

        public ParagraphType SelectedParagraph { get; set; }

        public Guid SelectedBeneficiary { get; set; }

        public int SalaryMonth { get; set; }

        public int SalaryYear { get; set; }
    }
}
