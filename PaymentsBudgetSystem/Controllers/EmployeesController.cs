using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models.Employees;
    using Core.Contracts;
    using Extensions;
    using Core.Models.Enums;

    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeesController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Info(
            string? firstName, 
            string? lastName,
            string? egn,
            int sortAttribute, 
            int sortBy)
        {
            var model = new AllEmployeesViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Egn = egn,                
                SortAttribute = (EmployeeSort)sortAttribute,
                SortBy = (SortBy)sortBy
            };

            model = await employeeService.GetAllEmployeesAsync(User.Id(), model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Info(AllEmployeesViewModel model)
        {
            return RedirectToAction(nameof(Info), new
            {
                firstName = model.FirstName,
                lastName = model.LastName,
                egn = model.Egn,
                sortAttribute = (int)model.SortAttribute,
                sortBy = (int)model.SortBy
            });
        }
    }
}
