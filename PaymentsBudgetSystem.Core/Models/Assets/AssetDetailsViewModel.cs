using PaymentsBudgetSystem.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Assets
{
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class AssetDetailsViewModel
    {
        public string Name { get; set; } = null!;

        public Guid AssetId { get; set; }   

        public DateTime DateAquired { get; set; }

        public ParagraphType ParagraphType { get; set; }

        [Range(YearMinValue, YearMaxValue, ErrorMessage = InvalidYearError)]
        public int Year { get; set; }

        public decimal ReportValue { get; set; }

        public Guid BeneficiaryId { get; set; }

        public string BeneficiaryName { get; set; } = null!;

        public List<AssetInfoViewModel> AssetMonthlyStatus { get; set; } = new();
    }
}
