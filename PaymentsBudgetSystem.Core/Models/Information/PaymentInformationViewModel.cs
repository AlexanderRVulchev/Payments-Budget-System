namespace PaymentsBudgetSystem.Core.Models.Information
{
    using Core.Models.Enums;
    using Data.Entities.Enums;
    using System.ComponentModel.DataAnnotations;

    public class PaymentInformationViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SortBy SortBy { get; set; }

        public InformationSort InformationSort { get; set; }

        public PaymentType PaymentType { get; set; }

        public string ReceiverNameFilter { get; set; } = String.Empty;

        public decimal? AmountMin { get; set; }

        public decimal? AmountMax { get; set; }

        public int Page { get; set; }

        public int NumberOfPages { get; set; }

        public List<PaymentInformationItemModel> Payments { get; set; } = new();
    }
}
