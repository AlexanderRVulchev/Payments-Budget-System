﻿using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models;
    using Core.Models.Beneficiaries;
    using Extensions;

    public class GatewayController : Controller
    {
        private readonly IBeneficiaryService beneficiaryService;

        public GatewayController(IBeneficiaryService _beneficiaryService)
        {
            beneficiaryService = _beneficiaryService;
        }

        [HttpGet]
        public async Task<IActionResult> ChoosePayment()
        {
            var allUserBeneficiaries = await beneficiaryService
                .GetAllBeneficiariesAsync(User.Id(), new AllBeneficiariesViewModel());

            var model = new ChoosePaymentViewModel
            {
                Beneficiaries = allUserBeneficiaries.Beneficiaries
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult NewSupportPayment(ChoosePaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ChoosePayment));
            }

            return RedirectToAction("Payment", "Support", new { id = model.SelectedBeneficiary, type = model.SelectedParagraph });
        }
    }
}
