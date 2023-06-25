using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Assets
{
    using Enums;

    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class AllAssetsViewModel
    {
        public List<AssetInfoViewModel> Assets { get; set; } = new();

        [Display(Name = "Име")]
        public string? NameFilter { get; set; }

        [Display(Name = "Година на справката")]
        [Range(YearMinValue, YearMaxValue, ErrorMessage = InvalidYearError)]
        public int InfoYear { get; set; }

        public int InfoMonth { get; set; }

        public AssetSort SortAttribute { get; set; }

        public SortBy SortBy { get; set; }
    }
}
