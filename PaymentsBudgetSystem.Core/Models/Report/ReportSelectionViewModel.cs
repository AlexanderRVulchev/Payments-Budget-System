using PaymentsBudgetSystem.Core.Models.User;

namespace PaymentsBudgetSystem.Core.Models.Report
{
    public class ReportSelectionViewModel
    {
        public string? SelectedInstitutionId { get; set; }

        public string? SelectedInstitutionName { get; set; }

        public List<InstitutionSelectModel> Institutions { get; set; } = new();

        public ReportInquiryViewModel ReportAnnotationCollection { get; set; } = new();
    }
}