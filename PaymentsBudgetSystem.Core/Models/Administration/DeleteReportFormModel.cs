using PaymentsBudgetSystem.Core.Models.Report;

namespace PaymentsBudgetSystem.Core.Models.Administration
{
    public class DeleteReportFormModel
    {
        public Guid ReportToDeleteId { get; set; }

        public List<ReportInquiryViewModel> InstitutionsWithReports { get; set; } = new();
    }
}
