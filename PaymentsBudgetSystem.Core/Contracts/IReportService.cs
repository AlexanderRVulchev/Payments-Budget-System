
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Report;

    public interface IReportService
    {
        Task AddReportAnnotations(string userId, ReportInquiryViewModel model);

        Task<ReportDataModel> BuildIndividualReport(string userId, int year, int month);

        Task<ReportDataModel> BuildConsolidatedReport(string userId, int year, int month);

        Task SaveIndividualReportAsync(string userId, ReportDataModel model);

        Task<ReportDataModel> GetReportById(Guid id);
    }
}
