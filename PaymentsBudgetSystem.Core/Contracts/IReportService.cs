
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Report;

    public interface IReportService
    {
        Task<ReportDataModel> BuildIndividualReport(string userId, int year, int month);

        Task<ReportDataModel> BuildConsolidatedReport(string userId, int year, int month);

        Task SaveIndividualReportAsync(string userId, ReportDataModel model);
    }
}
