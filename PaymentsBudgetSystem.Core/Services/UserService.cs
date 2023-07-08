using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Data;
    using Data.Entities;
    using Core.Models.Report;
    using static Common.RoleNames;
    using PaymentsBudgetSystem.Core.Models.User;

    public class UserService : IUserService
    {
        private PBSystemDbContext context;

        private RoleManager<IdentityRole> roleManager;

        private UserManager<User> userManager;

        private IBudgetService budgetService;

        public UserService(
            PBSystemDbContext _context,
            RoleManager<IdentityRole> _roleManager,
            UserManager<User> _userManager,
            IBudgetService _budgetService)
        {
            this.context = _context;
            this.roleManager = _roleManager;
            this.userManager = _userManager;
            this.budgetService = _budgetService;
        }

        public async Task<ICollection<string>> GetPrimaryNamesAsync()
        {
            var users = await context.Users
                .ToArrayAsync();

            List<string> primaryNames = new();

            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, PrimaryRoleName))
                {
                    primaryNames.Add(user.Name);
                }
            }

            return primaryNames
                .OrderBy(u => u)
                .ToArray();
        }

        public async Task<Dictionary<string, string>> GetPrimaryIdsAndNamesAsync()
        {
            var users = await context.Users
                .ToArrayAsync();

            Dictionary<string, string> primaryIdsAndNames = new();

            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, PrimaryRoleName))
                {
                    primaryIdsAndNames[user.Id] = user.Name;
                }
            }

            return primaryIdsAndNames
                .OrderBy(u => u.Value)
                .ToDictionary(u => u.Key, u => u.Value);
        }

        public async Task RelateSecondaryToPrimaryUserAsync(string primaryId, string secondaryId)
        {
            var userPair = new UserDependancy
            {
                PrimaryUserId = primaryId,
                SecondaryUserId = secondaryId
            };

            await budgetService.CreateBlankBudgetsForSecondaryUser(primaryId, secondaryId);

            await context.UsersDependancies.AddAsync(userPair);
            await context.SaveChangesAsync();
        }

        public async Task<List<InstitutionSelectModel>> GetAllUsersWithSavedReportsAsync()
            => await context
                .Users
                .Where(u => u.Reports.Any())
                .Select(u => new InstitutionSelectModel
                {
                    UserId = u.Id,
                    InstitutionName = u.Name
                })
                .ToListAsync();
    }
}
