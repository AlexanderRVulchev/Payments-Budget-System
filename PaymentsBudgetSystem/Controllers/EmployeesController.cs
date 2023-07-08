using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Employees;
    using Core.Models.Enums;
    using Extensions;

    using static Common.ExceptionMessages.Employee;
    using static Common.ValidationErrors.General;

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
            int sortBy,
            int page)
        {
            var model = new AllEmployeesViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Egn = egn,                
                SortAttribute = (EmployeeSort)sortAttribute,
                SortBy = (SortBy)sortBy,
                Page = page
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
                sortBy = (int)model.SortBy,
                page = model.Page
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new EmployeeFormModel();

            return View(model);
        }

        public async Task<IActionResult> Add(EmployeeFormModel model)
        {
            decimal minimumWage = await employeeService.GetMinimumWageAsync();

            if (model.MonthlySalary < minimumWage)
            {
                ModelState.AddModelError("", String.Format(SalaryIsBelowMinimumWage, minimumWage.ToString("n")));
            };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await employeeService.AddEmployeeAsync(User.Id(), model);

            return RedirectToAction(nameof(Info));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await employeeService.GetEmployeesAsync(User.Id(), id);
                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeFormModel model)
        {
            if (model.DateLeft != null &&
                model.DateLeft < model.DateEmployed)
            {
                ModelState.AddModelError("", EmployeeInvalidDateLeft);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await employeeService.EditEmployeeAsync(User.Id(), model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }

            return RedirectToAction(nameof(Info));
        }
    }
}
