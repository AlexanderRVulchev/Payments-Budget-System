using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Core.Contracts;
    using Core.Models.Employees;
    using Data;
    using Core.Models.Enums;
    using Data.Entities;
    using System;

    using static Common.ExceptionMessages.Employee;

    public class EmployeeService : IEmployeeService
    {
        private readonly PBSystemDbContext context;

        public EmployeeService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task AddEmployeeAsync(string userId, EmployeeFormModel model)
        {
            var entry = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Egn = model.Egn,
                DateEmployed = model.DateEmployed,
                MonthlySalary = model.MonthlySalary,
                DateLeft = null,
                UserId = userId,
                ContractType = model.ContractType
            };

            await context.Employees.AddAsync(entry);
            await context.SaveChangesAsync();
        }

        public async Task EditEmployeeAsync(string userId, EmployeeFormModel model)
        {
            var entity = await context
                .Employees
                .FindAsync(model.Id);

            if (entity == null)
            {
                throw new InvalidOperationException(EmployeeDoesNotExist);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(EmployeeAccessDenied);
            }

            entity.MonthlySalary = model.MonthlySalary;
            entity.DateLeft = model.DateLeft;

            await context.SaveChangesAsync();
        }

        public async Task<AllEmployeesViewModel> GetAllEmployeesAsync(string userId, AllEmployeesViewModel model)
        {
            var employees = context
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
                .AsQueryable();

            if (model.FirstName != null)
            {
                employees = employees.Where(e => e.FirstName.Contains(model.FirstName));
            }
            if (model.LastName != null)
            {
                employees = employees.Where(e => e.LastName.Contains(model.LastName));
            }
            if (model.Egn != null)
            {
                employees = employees.Where(e => e.Egn.Contains(model.Egn));
            }

            if (model.SortBy == SortBy.Ascending)
            {
                switch (model.SortAttribute)
                {
                    case EmployeeSort.FirstName:
                        employees = employees.OrderBy(e => e.FirstName); break;
                    case EmployeeSort.LastName:
                        employees = employees.OrderBy(e => e.LastName); break;
                    case EmployeeSort.Salary:
                        employees = employees.OrderBy(e => e.MonthlySalary); break;
                    case EmployeeSort.Egn:
                        employees = employees.OrderBy(e => e.Egn); break;
                    case EmployeeSort.DateEmployed:
                        employees = employees.OrderBy(e => e.DateEmployed); break;
                    case EmployeeSort.TotalIncome:
                        employees = employees.OrderBy(e => e.TotalIncome); break;
                    default: break;
                }
            }
            else
            {
                switch (model.SortAttribute)
                {
                    case EmployeeSort.FirstName:
                        employees = employees.OrderByDescending(e => e.FirstName); break;
                    case EmployeeSort.LastName:
                        employees = employees.OrderByDescending(e => e.LastName); break;
                    case EmployeeSort.Salary:
                        employees = employees.OrderByDescending(e => e.MonthlySalary); break;
                    case EmployeeSort.Egn:
                        employees = employees.OrderByDescending(e => e.Egn); break;
                    case EmployeeSort.DateEmployed:
                        employees = employees.OrderByDescending(e => e.DateEmployed); break;
                    case EmployeeSort.TotalIncome:
                        employees = employees.OrderByDescending(e => e.TotalIncome); break;
                    default: break;
                }
            }

            model.Employees = await employees.ToListAsync();

            return model;
        }

        public async Task<EmployeeFormModel> GetEmployeesAsync(string userId, Guid employeeId)
        {
            var entity = await context
                .Employees
                .FindAsync(employeeId);

            if (entity == null)
            {
                throw new InvalidOperationException(EmployeeDoesNotExist);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(EmployeeAccessDenied);
            }

            return new EmployeeFormModel
            {
                Id = entity.Id,
                DateEmployed = entity.DateEmployed,
                DateLeft = entity.DateLeft,
                ContractType = entity.ContractType,
                Egn = entity.Egn,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MonthlySalary = entity.MonthlySalary
            };
        }
    }
}
