namespace PaymentsBudgetSystem.Core.Models.Information
{
    using Core.Models.Enums;
    using Data.Entities.Enums;

    public class PaymentInformationViewModel
    {
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public SortBy SortBy { get; set; }

        public InformationSort InformationSort { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public PaymentType PaymentType { get; set; }

        public decimal? AmountMin { get; set; }

        public decimal? AmountMax { get; set; }

        public List<PaymentInformationItemModel> Payments { get; set; } = new();
    }
}
