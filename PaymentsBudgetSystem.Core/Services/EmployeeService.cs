using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Core.Contracts;
    using Core.Models.Employees;
    using Data;

    public class EmployeeService : IEmployeeService
    {
        private readonly PBSystemDbContext context;

        public EmployeeService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<AllEmployeesViewModel> GetAllEmployeesAsync(string userId, AllEmployeesViewModel model)
        {
            model.Employees = await context
                .Employees
                .Where(e => e.UserId == userId)
                .Include(e => e.SalaryDetails)
                .Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateEmployed = e.DateEmployed,
                    DateLeft = e.DateLeft,
                    ContractType = e.ContractType,
                    MonthlySalary = e.MonthlySalary,
                    Egn = e.Egn,
                    TotalIncome = e.SalaryDetails.Sum(sd =>
                        sd.InsurancePension +
                        sd.InsuranceHealth +
                        sd.InsuranceAdditional +
                        sd.NetSalaryJobContract +
                        sd.NetSalaryStateOfficial)
                })
                .ToListAsync();

            return model;
        }
    }
}
