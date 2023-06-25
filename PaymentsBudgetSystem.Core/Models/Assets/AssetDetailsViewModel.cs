using PaymentsBudgetSystem.Data.Entities.Enums;

namespace PaymentsBudgetSystem.Core.Models.Assets
{
    public class AssetDetailsViewModel
    {
        public string Name { get; set; } = null!;

        public Guid AssetId { get; set; }   

        public DateTime DateAquired { get; set; }

        public ParagraphType ParagraphType { get; set; }

        public int Year { get; set; }

        public decimal ReportValue { get; set; }

        public Guid BeneficiaryId { get; set; }

        public string BeneficiaryName { get; set; } = null!;

        public List<AssetInfoViewModel> AssetMonthlyStatus { get; set; } = new();
    }
}
