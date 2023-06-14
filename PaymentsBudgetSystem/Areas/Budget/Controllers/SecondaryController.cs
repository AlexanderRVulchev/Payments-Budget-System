using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PaymentsBudgetSystem.Areas.Budget.Controllers
{
    using Core.Contracts;
    using Core.Models.Budget;
    using Extensions;

    using static Common.RoleNames;

    [Area("Budget")]
    [Authorize(Roles = SecondaryRoleName)]
    public class SecondaryController : Controller
    {
        private readonly IBudgetService budgetService;

        public SecondaryController(IBudgetService _budgetService)
        {
            this.budgetService = _budgetService;
        }

        public async Task<IActionResult> Info()
        {
            IEnumerable<BudgetViewModel> model = await budgetService.GetIndividualBudgetsAsync(User.Id());
            return View(model);
        }
    }
}
