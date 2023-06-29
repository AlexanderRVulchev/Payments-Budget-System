﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Salaries;
    using Extensions;
    using Microsoft.CodeAnalysis.Operations;

    [Authorize]
    public class SalariesController : Controller
    {
        private readonly IPaymentService paymentService;

        public SalariesController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Payment(int year, int month)
        {
            SalariesPaymentViewModel model = await paymentService.CreatePayroll(User.Id(), year, month);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(SalariesPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model = await paymentService.CreatePayroll(User.Id(), model.Year, model.Month);

            var paymentId = await paymentService.AddNewSalariesPayment(User.Id(), model);

            return RedirectToAction(nameof(Details), new {id = paymentId});
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var model = paymentService.GetSalariesDetailsById(User.Id(), id);
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
