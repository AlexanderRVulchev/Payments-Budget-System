using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Salaries;
    using Extensions;
    using static Common.RoleNames;

    [Authorize(Roles = PrimaryAndSecondaryRoleNames)]
    public class SalariesController : Controller
    {
        private readonly IPaymentService paymentService;

        public SalariesController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }


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

            try
            {
                var paymentId = await paymentService.AddNewSalariesPayment(User.Id(), model);

                TempData["SuccessMessage"] = $"Заплатите са изплатени успешно!";

                return RedirectToAction(nameof(Details), new { id = paymentId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(nameof(Payment), model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var model = await paymentService.GetSalariesDetailsById(User.Id(), id);

                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }
    }
}
