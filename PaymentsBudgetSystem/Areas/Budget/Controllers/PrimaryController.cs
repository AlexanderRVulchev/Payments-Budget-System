using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PaymentsBudgetSystem.Areas.Budget.Controllers
{
    using Core.Contracts;
    using Core.Models.Budget;
    using Extensions;
    using static Common.RoleNames;
    using static Common.ExceptionMessages.Budget;
    using Microsoft.CodeAnalysis.Operations;

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
            var model = await GetPrimaryBudgetsModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Info(PrimaryBudgetsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var returnModel = await GetPrimaryBudgetsModel();
                returnModel.NewBudgetYear = model.NewBudgetYear;
                returnModel.NewBudgetFunds = model.NewBudgetFunds;
                return View(returnModel);
            }

            try
            {
                await budgetService.AddNewConsolidatedBudget(User.Id(), model.NewBudgetYear, model.NewBudgetFunds);
            }
            catch (InvalidOperationException ex)
            {
                var returnModel = await GetPrimaryBudgetsModel();

                returnModel.NewBudgetYear = model.NewBudgetYear;
                returnModel.NewBudgetFunds = model.NewBudgetFunds;

                ModelState.AddModelError("", ex.Message);
                return View(returnModel);
            }

            return RedirectToAction(nameof(Info));
        }

        [HttpGet]
        public async Task<IActionResult> EditBudget(int year)
        {
            try
            {
                EditBudgetFormModel model = await budgetService.GetConsolidatedBudgetDataForEditAsync(User.Id(), year);
                return View(model);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = CannotRetrieveConsolidatedBudget });
            }


        }

        [HttpPost]
        public async Task<IActionResult> EditBudget(EditBudgetFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var returnModel = await budgetService.GetConsolidatedBudgetDataForEditAsync(User.Id(), model.FiscalYear);                
                return View(returnModel);
            }

            var retModel = await budgetService.GetConsolidatedBudgetDataForEditAsync(User.Id(), model.ConsolidatedBudget.FiscalYear);
            return View(retModel);
        }

        private async Task<PrimaryBudgetsViewModel> GetPrimaryBudgetsModel()
        {
            IEnumerable<BudgetViewModel> individualBudgets = await budgetService.GetIndividualBudgetsAsync(User.Id());
            IEnumerable<ConsolidatedBudgetViewModel> consolidatedBudgets = await budgetService.GetConsolidatedBudgetsAsync(User.Id());

            var model = new PrimaryBudgetsViewModel
            {
                IndividualBudgets = individualBudgets.ToList(),
                ConsolidatedBudgets = consolidatedBudgets.ToList()
            };

            return model;
        }
    }
}

