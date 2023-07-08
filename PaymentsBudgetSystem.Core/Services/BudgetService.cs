using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Core.Models.Budget;
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Authentication;
    using static Common.ExceptionMessages.Budget;

    public class BudgetService : IBudgetService
    {
        private readonly PBSystemDbContext context;

        private readonly IReportService reportService;

        public BudgetService(
            PBSystemDbContext _context,
            IReportService _reportService)
        {
            context = _context;
            reportService = _reportService;
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

        public async Task<EditBudgetFormModel> GetFullConsolidatedBudgetForPrimaryAsync(string userId, int year)
        {
            var consolidatedBudget = await context.ConsolidatedBudgets
               .Include(b => b.User)
               .Where(b => b.UserId == userId && b.FiscalYear == year)
               .FirstOrDefaultAsync()
                    ?? throw new ArgumentNullException("", CannotRetrieveConsolidatedBudget);

            string[] secondaryUsersIds = await context.UsersDependancies
               .Where(ud => ud.PrimaryUserId == userId)
               .Select(ud => ud.SecondaryUserId)
               .ToArrayAsync();

            var individualBudgets = await context.IndividualBudgets
                .Where(b => secondaryUsersIds.Contains(b.UserId) && b.FiscalYear == year
                    || b.UserId == userId && b.FiscalYear == year)
                .Include(b => b.User)
                .ToArrayAsync();

            decimal totalAllocatedFunds = 0;
            List<IndividualBudgetFormData> secondaryBudgetModels = new();

            foreach (var individualBudget in individualBudgets
                    .Where(b => b.FiscalYear == consolidatedBudget.FiscalYear))
            {
                totalAllocatedFunds += individualBudget.SalariesLimit + individualBudget.SupportLimit + individualBudget.AssetsLimit;

                var annualReport = await reportService.BuildIndividualReport(individualBudget.UserId, year, 12);

                decimal annualSalaryExpenses = 
                      annualReport.Bank0101
                    + annualReport.Bank0102
                    + annualReport.Transfer0551
                    + annualReport.Transfer0560
                    + annualReport.Transfer0580
                    + annualReport.Transfer0590;

                decimal annualSupportExpenses =
                      annualReport.Bank1015
                    + annualReport.Bank1020
                    + annualReport.Bank1051
                    + annualReport.Cash1015
                    + annualReport.Cash1020
                    + annualReport.Cash1051;

                decimal annualAssetsExpenses = 
                      annualReport.Bank5100
                    + annualReport.Bank5200
                    + annualReport.Bank5300;

                secondaryBudgetModels.Add(new IndividualBudgetFormData
                {
                    Id = individualBudget.Id,
                    FiscalYear = individualBudget.FiscalYear,
                    Name = individualBudget.User.Name,
                    SalariesLimit = individualBudget.SalariesLimit,
                    SupportLimit = individualBudget.SupportLimit,
                    AssetsLimit = individualBudget.AssetsLimit,
                    SalariesExpenses = annualSalaryExpenses,
                    SupportExpenses = annualSupportExpenses,
                    AssetsExpenses = annualAssetsExpenses
                });
            }

            var budgetModel = GenerateConsolidatedBudget(consolidatedBudget, totalAllocatedFunds, userId);

            return new EditBudgetFormModel
            {
                ConsolidatedBudget = budgetModel,
                IndividualBudgetsData = secondaryBudgetModels,
                FiscalYear = consolidatedBudget.FiscalYear
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
                .Where(b => secondaryUsersIds.Contains(b.UserId) || b.UserId == userId)
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

                var budgetModel = GenerateConsolidatedBudget(consolidatedBudget, totalAllocatedFunds, userId);

                consolidatedBudgetViewModels.Add(budgetModel);
            }

            return consolidatedBudgetViewModels;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetIndividualBudgetsAsync(string userId)
        {
            var budgetViewModel = await context
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
                    Name = b.User.Name,
                })
                .ToArrayAsync();

            return budgetViewModel;
        }

        private ConsolidatedBudgetViewModel GenerateConsolidatedBudget(
                ConsolidatedBudget consolidatedBudget,
                decimal totalAllocatedFunds,
                string userId)
            => new ConsolidatedBudgetViewModel
            {
                Name = consolidatedBudget.User.Name,
                FiscalYear = consolidatedBudget.FiscalYear,
                Id = consolidatedBudget.Id,
                TotalLimit = consolidatedBudget.TotalLimit,
                UserId = userId,
                Allocated = totalAllocatedFunds,
                Unallocated = consolidatedBudget.TotalLimit - totalAllocatedFunds
            };

        public async Task EditBudgetAsync(EditBudgetFormModel model)
        {
            IndividualBudget individualBudget = await context
                .IndividualBudgets
                .FindAsync(model.Id)
                    ?? throw new InvalidOperationException(CannotRetrieveIndividualBudget);

            individualBudget.SalariesLimit = model.NewSalaryLimit;
            individualBudget.SupportLimit = model.NewSupportLimit;
            individualBudget.AssetsLimit = model.NewAssetsLimit;

            await context.SaveChangesAsync();
        }

        public async Task CreateBlankBudgetsForSecondaryUser(string primaryId, string secondaryId)
        {
            int[] yearsWherePrimaryUserHasBudgets = await context
                .ConsolidatedBudgets
                .Where(b => b.UserId == primaryId)
                .Select(b => b.FiscalYear)
                .ToArrayAsync();

            List<IndividualBudget> secondaryIndividualBudgets = new();

            foreach (var year in yearsWherePrimaryUserHasBudgets)
            {
                secondaryIndividualBudgets.Add(new IndividualBudget
                {
                    UserId = secondaryId,
                    FiscalYear = year
                });
            }

            await context.IndividualBudgets.AddRangeAsync(secondaryIndividualBudgets);
            await context.SaveChangesAsync();
        }
    }
}
