namespace PaymentsBudgetSystem.Core.Models.Assets
{
    public class AssetShortViewModel
    {
        public Guid AssetId { get; set; }

        public DateTime AssetAquired { get; set; }

        public DateTime? AssetDisposed { get; set; }

        public string Description { get; set; } = null!;

        public decimal ReportValue { get; set; }        
    }
}
