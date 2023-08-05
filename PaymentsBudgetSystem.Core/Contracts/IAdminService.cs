namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Administration;

    public interface IAdminService
    {
        /// <summary>
        /// Retrieves from the database all settings, used for salary calculations, calculations for the amortization plan of assets and minimum wage, which can be altered by the Admin
        /// </summary>
        /// <returns>A model, containing all settings' values</returns>
        Task<GlobalSettingsEditModel> GetGlobalSettingsAsync();

        /// <summary>
        /// Persists all setting values from the given model in the database. If a value represents a percentage, it is divided by 100 before being persisted.
        /// </summary>
        /// <param name="model"></param>
        Task SaveGlobalSettingsAsync(GlobalSettingsEditModel model);

        /// <summary>
        /// Retrives all reports with their Ids, grouped by institutions, along with information about the month and year each report relates to
        /// </summary>
        /// <returns>A DeleteReportFormModel containing all reports' Ids, and their respective year and month</returns>
        Task<DeleteReportFormModel> GetAllReportsAsync();

        /// <summary>
        /// Deletes a report from the database, if it exists
        /// </summary>
        /// <param name="reportId">The Id of the report about to be deleted</param>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteReportByIdAsync(Guid reportId);
    }
}
