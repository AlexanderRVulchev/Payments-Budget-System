﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models.Cash;
    using Core.Contracts;
    using Extensions;

    [Authorize]
    public class CashPaymentController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IPaymentService paymentsService;

        public CashPaymentController(
            IEmployeeService _employeeService,
            IPaymentService _paymentsService)
        {
            employeeService = _employeeService;
            paymentsService = _paymentsService;
        }

        [HttpGet]
        public async Task<IActionResult> Payment()
        {
            var model = new CashPaymentViewModel
            {
                Employees = await employeeService.GetEmployeeListAsync(User.Id())
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(CashPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Employees = await employeeService.GetEmployeeListAsync(User.Id());
                return View(model);
            }

            try
            {
                var paymentId = await paymentsService.AddNewCashPaymentAsync(User.Id(), model);
                return RedirectToAction(nameof(CashPaymentDetails), new { id = paymentId });
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }

        public async Task<IActionResult> CashPaymentDetails(Guid id)
        {
            try
            {
                var model = await paymentsService.GetCashPaymentById(User.Id(), id);
                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }
    }
}
