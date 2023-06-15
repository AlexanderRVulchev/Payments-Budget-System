using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Core.Models.Budget;
    using Data;
    using Microsoft.EntityFrameworkCore;

    using static PaymentsBudgetSystem.Common.ExceptionMessages.Budget;

    public class BudgetService : IBudgetService
    {


        private readonly PBSystemDbContext context;

        public BudgetService(PBSystemDbContext _context)
        {
            this.context = _context;
        }

        public async Task<EditBudgetFormModel> GetConsolidatedBudgetDataForEditAsync(string userId, int year)
        {
            BudgetViewModel consolidatedBudget = await context
                .ConsolidatedBudgets
                .Include(x => x.User)
                .Where(b => b.FiscalYear == year)
                .Select(b => new BudgetViewModel
                {
                    Id = b.Id,
                    FiscalYear = b.FiscalYear,
                    AssetsLimit = b.AssetsLimit,
                    SalariesLimit = b.SalariesLimit,
                    SupportLimit = b.SupportLimit,
                    UserId = userId,
                    Name = b.User.Name
                })
                .FirstAsync()
                    ?? throw new InvalidOperationException();

            string[] secondaryUsersIds = await context.UsersDependancies
                .Where(ud => ud.PrimaryUserId == userId)
                .Select(ud => ud.SecondaryUserId)
                .ToArrayAsync();

            var primaryBudgets = new List<IndividualBudgetFormData>();

            //TODO: A report builder class

            return new EditBudgetFormModel
            {
                ConsolidatedBudget = consolidatedBudget

            };
        }

        public async Task<IEnumerable<BudgetViewModel>> GetConsolidatedBudgetsAsync(string userId)
            => await context
                .ConsolidatedBudgets
                .Where(b => b.UserId == userId)
                .Include(b => b.User)
                .Select(b => new BudgetViewModel
                {
                    AssetsLimit = b.AssetsLimit,
                    FiscalYear = b.FiscalYear,
                    Id = b.Id,
                    SalariesLimit = b.SalariesLimit,
                    SupportLimit = b.SupportLimit,
                    UserId = userId,
                    Name = b.User.Name
                })
                .ToArrayAsync();

        public async Task<IEnumerable<BudgetViewModel>> GetIndividualBudgetsAsync(string userId)
            => await context
                .IndividualBudgets
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .Select(b => new BudgetViewModel
                {
                    AssetsLimit = b.AssetsLimit,
                    FiscalYear = b.FiscalYear,
                    Id = b.Id,
                    SalariesLimit = b.SalariesLimit,
                    SupportLimit = b.SupportLimit,
                    Name = b.User.Name
                })
                .ToArrayAsync();
    }
}
