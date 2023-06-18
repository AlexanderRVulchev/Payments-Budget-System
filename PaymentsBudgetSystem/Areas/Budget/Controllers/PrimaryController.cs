﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Areas.Budget.Controllers
{
    using Core.Contracts;
    using Core.Models.Budget;
    using Extensions;
    using static Common.ExceptionMessages.Budget;
    using static Common.ValidationErrors.Budget;
    using static Common.RoleNames;
    using PaymentsBudgetSystem.Data.Migrations;

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
                EditBudgetFormModel model = await budgetService.GetFullConsolidatedBudgetForPrimaryAsync(User.Id(), year);
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
            var fullConsolidatedBudget = await budgetService.GetFullConsolidatedBudgetForPrimaryAsync(User.Id(), model.FiscalYear);

            if (!ModelState.IsValid)
            {
                return View(fullConsolidatedBudget);
            }

            var totalAllocatedFunds = model.IndividualBudgetsData
                .Where(b => b.Id != model.Id)
                .Sum(b => b.SupportLimit + b.SalariesLimit + b.AssetsLimit);

            totalAllocatedFunds += model.NewSalaryLimit + model.NewSupportLimit + model.NewAssetsLimit;
            decimal totalLimit = fullConsolidatedBudget.ConsolidatedBudget.TotalLimit;

            if (totalAllocatedFunds > totalLimit)
            {
                ModelState.AddModelError("", String.Format(ConsolidatedBudgetLimitExceeded, totalAllocatedFunds - totalLimit));
                return View(fullConsolidatedBudget);
            }

            await budgetService.EditBudgetAsync(model);
                             
            model = await budgetService.GetFullConsolidatedBudgetForPrimaryAsync(User.Id(), model.FiscalYear);

            return View(model);
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

