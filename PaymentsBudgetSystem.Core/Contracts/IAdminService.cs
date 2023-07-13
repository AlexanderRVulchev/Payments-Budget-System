namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Administration;
    using PaymentsBudgetSystem.Core.Models.Report;

    public interface IAdminService
    {
        Task<GlobalSettingsEditModel> GetGlobalSettingsAsync();

        Task SaveGlobalSettingsAsync(GlobalSettingsEditModel model);

        Task<DeleteReportFormModel> GetAllReportsAsync();

        Task DeleteReportByIdAsync(Guid reportId);
    }
}
