﻿
namespace PaymentsBudgetSystem.Core.Contracts
{
    using Core.Models.Budget;

    public interface IBudgetService
    {
        Task<IEnumerable<BudgetViewModel>> GetIndividualBudgetsAsync(string userId);

        Task<IEnumerable<ConsolidatedBudgetViewModel>> GetConsolidatedBudgetsAsync(string userId);

        Task<EditBudgetFormModel> GetConsolidatedBudgetDataForEditAsync(string userId, int year);

        Task AddNewConsolidatedBudget(string userId, int newBudgetYear, decimal newBudgetFunds);
    }
}