using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Info()
        {
            return View();
        }
    }
}
