
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Report;

    public interface IReportService
    {
        Task<IndividualReportDataModel> BuildIndividualReport(string userId, int year, int month);
    }
}
