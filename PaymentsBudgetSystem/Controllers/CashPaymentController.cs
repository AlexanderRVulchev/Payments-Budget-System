using Microsoft.AspNetCore.Mvc;
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
                return View(model);
            }

            return View(model);
        }
    }
}
