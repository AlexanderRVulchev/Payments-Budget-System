using PaymentsBudgetSystem.Core.Models.Employees;

namespace PaymentsBudgetSystem.Core.Contracts
{
    public interface IEmployeeService
    {
        Task<AllEmployeesViewModel> GetAllEmployeesAsync(string userId, AllEmployeesViewModel model);

        Task AddEmployeeAsync(string userId, EmployeeFormModel model);
    }
}
