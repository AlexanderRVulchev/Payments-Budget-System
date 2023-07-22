using System.ComponentModel.DataAnnotations;

namespace PaymentsBudgetSystem.Core.Models.Budget
{
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

    public class EditBudgetFormModel
    {
        public ConsolidatedBudgetViewModel ConsolidatedBudget { get; set; } = new();

        public List<IndividualBudgetFormData> IndividualBudgetsData { get; set; } = new();

        public int FiscalYear { get; set; }

        public Guid Id { get; set; }

        [Range(0, MoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Лимит заплати")]
        public decimal NewSalaryLimit { get; set; }

        [Range(0, MoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Лимит издръжка")]
        public decimal NewSupportLimit { get; set; }

        [Range(0, MoneyMaxValue, ErrorMessage = RangeValidationError)]
        [Display(Name = "Лимит Капиталови")]
        public decimal NewAssetsLimit { get; set; }
    }
}
