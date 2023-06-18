using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Core.Models.Budget;
    using Data;
    using Data.Entities;

    using static Common.ExceptionMessages.Budget;

    public class BudgetService : IBudgetService
    {
        private readonly PBSystemDbContext context;

        public BudgetService(PBSystemDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddNewConsolidatedBudget(string userId, int newBudgetYear, decimal newBudgetFunds)
        {
            bool budgetAlreadyExists = context.ConsolidatedBudgets
                .Any(cb => cb.UserId == userId && cb.FiscalYear == newBudgetYear);

            if (budgetAlreadyExists)
            {
                throw new InvalidOperationException(TheBudgetAlreadyExists);
            }

            var budgetToAdd = new ConsolidatedBudget
            {
                UserId = userId,
                FiscalYear = newBudgetYear,
                TotalLimit = newBudgetFunds
            };

            await context.ConsolidatedBudgets.AddAsync(budgetToAdd);

            string[] secondaryIds = await context.UsersDependancies
                .Where(ud => ud.PrimaryUserId == userId)
                .Select(ud => ud.SecondaryUserId)
                .ToArrayAsync();

            var newBudgetEntities = new List<IndividualBudget>();
            newBudgetEntities.Add(new IndividualBudget
            {
                UserId = userId,
                FiscalYear = newBudgetYear
            });

            foreach (var secondaryId in secondaryIds)
            {
                newBudgetEntities.Add(new IndividualBudget
                {
                    UserId = secondaryId,
                    FiscalYear = newBudgetYear
                });
            }

            await context.IndividualBudgets.AddRangeAsync(newBudgetEntities);
            await context.SaveChangesAsync();
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
                    Name = b.User.Name,
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
                ConsolidatedBudget = consolidatedBudget,


            };
        }

        public async Task<IEnumerable<ConsolidatedBudgetViewModel>> GetConsolidatedBudgetsAsync(string userId)
        {
            var consolidatedBudgets = await context.ConsolidatedBudgets
               .Include(b => b.User)
               .Where(b => b.UserId == userId)
               .ToArrayAsync();

            string[] secondaryUsersIds = await context.UsersDependancies
               .Where(ud => ud.PrimaryUserId == userId)
               .Select(ud => ud.SecondaryUserId)
               .ToArrayAsync();

            var individualBudgets = await context.IndividualBudgets
                .Where(b => secondaryUsersIds.Contains(b.UserId))
                .Include(b => b.User)
                .ToArrayAsync();

            var consolidatedBudgetViewModels = new List<ConsolidatedBudgetViewModel>();

            foreach (var consolidatedBudget in consolidatedBudgets)
            {
                decimal totalAllocatedFunds = 0;

                foreach (var individualBudget in individualBudgets
                    .Where(b => b.FiscalYear == consolidatedBudget.FiscalYear))
                {
                    totalAllocatedFunds += individualBudget.SalariesLimit + individualBudget.SupportLimit + individualBudget.AssetsLimit;
                }

                ConsolidatedBudgetViewModel budgetModel = new()
                {
                    Name = consolidatedBudget.User.Name,
                    FiscalYear = consolidatedBudget.FiscalYear,
                    Id = consolidatedBudget.Id,
                    TotalLimit = consolidatedBudget.TotalLimit,
                    UserId = userId,
                    Allocated = totalAllocatedFunds,
                    Unallocated = consolidatedBudget.TotalLimit - totalAllocatedFunds
                };

                consolidatedBudgetViewModels.Add(budgetModel);
            }

            return consolidatedBudgetViewModels;
        }

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
