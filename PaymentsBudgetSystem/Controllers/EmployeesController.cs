using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models.Employees;

    [Authorize]
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Info()
        {
            var model = new AllEmployeesViewModel();



            return View(model);
        }
    }
}
