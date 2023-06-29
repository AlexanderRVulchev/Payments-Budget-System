using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    public class CashPaymentController : Controller
    {
        public IActionResult Payment()
        {
            return View();
        }
    }
}
