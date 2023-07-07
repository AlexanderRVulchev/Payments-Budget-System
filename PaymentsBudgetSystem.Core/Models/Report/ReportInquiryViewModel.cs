namespace PaymentsBudgetSystem.Core.Models.Report
{
    public class ReportInquiryViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public bool IsConsolidated { get; set; }

        public string? InstitutionName { get; set; }

        public string? InstitutionId { get; set; }

        public List<ReportAnnotationViewModel> IndividualReports { get; set; } = new();

        public List<ReportAnnotationViewModel> ConsolidatedReports { get; set; } = new();
    }
}
