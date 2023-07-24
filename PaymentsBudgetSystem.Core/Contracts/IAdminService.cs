namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Administration;

    public interface IAdminService
    {
        Task<GlobalSettingsEditModel> GetGlobalSettingsAsync();

        Task SaveGlobalSettingsAsync(GlobalSettingsEditModel model);

        Task<DeleteReportFormModel> GetAllReportsAsync();

        Task DeleteReportByIdAsync(Guid reportId);
    }
}
