
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Core.Models.Budget;

    public interface IBudgetService
    {
        Task<IEnumerable<BudgetViewModel>> GetIndividualBudgetsAsync(string userId);

        Task<IEnumerable<BudgetViewModel>> GetConsolidatedBudgetsAsync(string userId);

        Task<EditBudgetFormModel> GetConsolidatedBudgetDataForEditAsync(string userId, int year);
    }
}
