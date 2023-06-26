using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Salaries;
    using Extensions;

    [Authorize]
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
    }
}
