namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Administration;

    public interface IAdminService
    {
        Task<GlobalSettingsEditModel> GetGlobalSettingsAsync();

        Task SaveGlobalSettingsAsync(GlobalSettingsEditModel model);
    }
}
