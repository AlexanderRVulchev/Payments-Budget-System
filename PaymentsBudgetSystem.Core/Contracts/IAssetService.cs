
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Models.Assets;

    public interface IAssetService
    {
        /// <summary>
        /// Retrieves from the database assets belonging to the user, which are about to be displayed on the selected page.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns>A model containing the user's sorted and paginated assets</returns>
        Task<AllAssetsViewModel> GetAllAssetsAsync(string userId, AllAssetsViewModel model);

        /// <summary>
        /// Retrieves information about the amortization plan of an asset with the given Id for the given year, if the asset belongs to the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="year"></param>
        /// <returns>A model with the asset's detailed information, beneficiary name and amortization values for each month.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<AssetDetailsViewModel> GetAssetDetailsAsync(string userId, Guid id, int year);
    }
}
