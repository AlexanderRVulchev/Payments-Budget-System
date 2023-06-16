using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PaymentsBudgetSystem.Areas.Budget.Controllers
{
    using Core.Contracts;
    using Core.Models.Budget;
    using Extensions;
    using static Common.RoleNames;
    using static Common.ExceptionMessages.Budget;

    [Area("Budget")]
    [Authorize(Roles = PrimaryRoleName)]
    public class PrimaryController : Controller
    {
        private readonly IBudgetService budgetService;

        public PrimaryController(IBudgetService _budgetService)
        {
            this.budgetService = _budgetService;
        }

        public async Task<IActionResult> Info()
        {
            IEnumerable<BudgetViewModel> individualBudgets = await budgetService.GetIndividualBudgetsAsync(User.Id());
            IEnumerable<BudgetViewModel> consolidatedBudgets = await budgetService.GetConsolidatedBudgetsAsync(User.Id());

            var model = new PrimaryBudgetsViewModel
            {
                IndividualBudgets = individualBudgets.ToList(),
                ConsolidatedBudgets = consolidatedBudgets.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Info(PrimaryBudgetsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = InvalidBudgetYearOrFunds });
            }

            try
            {
                await budgetService.AddNewConsolidatedBudget(User.Id(), model.NewBudgetYear, model.NewBudgetFunds);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }

            return RedirectToAction(nameof(Info));
        }

        [HttpGet]
        public async Task<IActionResult> EditBudget(int year)
        {
            EditBudgetFormModel model;

            try
            {
                model = await budgetService.GetConsolidatedBudgetDataForEditAsync(User.Id(), year);
                return View(model);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = CannotRetrieveConsolidatedBudget });
            }
        }

    }
}

