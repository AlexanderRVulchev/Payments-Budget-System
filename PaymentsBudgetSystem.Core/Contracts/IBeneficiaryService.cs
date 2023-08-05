
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Core.Models.Beneficiaries;

    public interface IBeneficiaryService
    {
        /// <summary>
        /// Retrives user's beneficiaries which are about to be displayed on the page and populates the model with them.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns>A model, containing filtered, sorted and paginated beneficiaries.</returns>
        Task<AllBeneficiariesViewModel> GetAllBeneficiariesAsync(string userId, AllBeneficiariesViewModel model);

        /// <summary>
        /// Creates and saves a new beneficiary for the user with the data, provided in the model, 
        /// if the beneficiary doesn't already exist.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddBeneficiaryAsync(string userId, BeneficiaryFormModel model);

        /// <summary>
        /// Retrieves a beenficiary with the given Id if it exists and belongs to the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="beneficiaryId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<BeneficiaryFormModel> GetBeneficiaryAsync(string userId, Guid beneficiaryId);

        /// <summary>
        /// Updates a beneficiary with the user's new input data if the beneficiary exists and belongs to the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditBeneficiaryAsync(string userId, BeneficiaryFormModel model);
    }
}
