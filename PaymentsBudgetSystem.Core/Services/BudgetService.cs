using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Core.Models.Budget;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class BudgetService : IBudgetService
    {
        private readonly PBSystemDbContext context;

        public BudgetService(PBSystemDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetConsolidatedBudgetsAsync(string userId)
            => await context
                .ConsolidatedBudgets
                .Where(b => b.UserId == userId)
                .Select(b => new BudgetViewModel
                {
                    AssetsLimit = b.AssetsLimit,
                    FiscalYear = b.FiscalYear,
                    Id = b.Id,
                    SalariesLimit = b.SalariesLimit,
                    SupportLimit = b.SupportLimit,
                    UserId = userId
                })
                .ToArrayAsync();

        public async Task<IEnumerable<BudgetViewModel>> GetIndividualBudgetsAsync(string userId)
            => await context
                .IndividualBudgets
                .Where(b => b.UserId == userId)
                .Select(b => new BudgetViewModel
                {
                    AssetsLimit = b.AssetsLimit,
                    FiscalYear = b.FiscalYear,
                    Id = b.Id,
                    SalariesLimit = b.SalariesLimit,
                    SupportLimit = b.SupportLimit,
                })
                .ToArrayAsync();
            
        
    }
}
