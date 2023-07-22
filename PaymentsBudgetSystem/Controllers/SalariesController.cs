using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Salaries;
    using Extensions;

    using static Common.RoleNames;
    using static Common.DataConstants.General;
    using static Common.ValidationErrors.General;

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
            if (year < YearMinValue || year > YearMaxValue)
            {
                ModelState.AddModelError("", String.Format(InvalidYearError, null, YearMinValue, YearMaxValue));
                year = DateTime.Now.Year;
            }
            if (month < 1 || month > 12)
            {
                ModelState.AddModelError("", InvalidMonthError);
                month = 1;
            }

            SalariesPaymentViewModel model = await paymentService.CreatePayrollAsync(User.Id(), year, month);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(SalariesPaymentViewModel model)
        {
            if (model.Amount < 0.01m)
            {
                ModelState.AddModelError("", PaymentMoneyCannotBeZeroOrLess);
                return View(nameof(Payment), model);
            }

            model = await paymentService.CreatePayrollAsync(User.Id(), model.Year, model.Month);

            try
            {
                var paymentId = await paymentService.AddNewSalariesPaymentAsync(User.Id(), model);

                TempData["SuccessMessage"] = "Заплатите са изплатени успешно!";

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
                var model = await paymentService.GetSalariesDetailsByIdAsync(User.Id(), id);

                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }
    }
}
