
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Report;

    public interface IReportService
    {
        Task AddReportAnnotationsAsync(string userId, ReportInquiryViewModel model);

        Task<ReportDataModel> BuildIndividualReportAsync(string userId, int year, int month);

        Task<ReportDataModel> BuildConsolidatedReportAsync(string userId, int year, int month);

        Task SaveIndividualReportAsync(string userId, ReportDataModel model);

        Task<ReportDataModel> GetReportByIdAsync(Guid id);
    }
}
