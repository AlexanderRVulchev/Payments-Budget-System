
namespace PaymentsBudgetSystem.Core.Models.Support
{
    using Core.Models.Beneficiaries;
    using Data.Entities.Enums;

    public class NewSupportPaymentViewModel
    {
        public List<BeneficiaryViewModel> Beneficiaries { get; set; } = new();

        public ParagraphType SelectedParagraph { get; set; }

        public Guid SelectedBeneficiary { get; set; }
    }
}
