
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Core.Models.Budget;

    public interface IBudgetService
    {
        Task<IEnumerable<BudgetViewModel>> GetIndividualBudgetsAsync(string userId);

        Task<IEnumerable<ConsolidatedBudgetViewModel>> GetConsolidatedBudgetsAsync(string userId);

        Task<EditBudgetFormModel> GetFullConsolidatedBudgetForPrimaryAsync(string userId, int year);

        Task AddConsolidatedBudgetAsync(string userId, int newBudgetYear, decimal newBudgetFunds);

        Task EditBudgetAsync(EditBudgetFormModel model);

        Task CreateBlankBudgetsForSecondaryUser(string primaryId, string secondaryId);
    }
}
