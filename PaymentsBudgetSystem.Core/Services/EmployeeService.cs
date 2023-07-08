using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Core.Contracts;
    using Core.Models.Employees;
    using Data;
    using Data.Entities;
    using PaymentsBudgetSystem.Core.Helpers;
    using System;
    using System.Collections.Generic;

    using static Common.ExceptionMessages.Employee;
    using static Common.DataConstants.General;
    using PaymentsBudgetSystem.Core.Models.Enums;
    using GlobalSetting = Models.Enums.GlobalSetting;

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
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Egn = model.Egn;

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
                        sd.InsurancePensionEmployer + 
                        sd.InsurancePensionEmployee +
                        sd.InsuranceHealthEmployer +
                        sd.InsuranceHealthEmployee +
                        sd.InsuranceAdditionalEmployer +
                        sd.InsuranceAdditionalEmployee +
                        sd.IncomeTax +
                        sd.NetSalaryJobContract +
                        sd.NetSalaryStateOfficial)
                })
                .AsQueryable();

            Sorter sorter = new();

            employees = sorter.SortEmployees(employees, model);

            model.Employees = await employees.ToListAsync();

            model.NumberOfPages = (model.Employees.Count / ItemsPerPage);

            if (model.Employees.Count % ItemsPerPage > 0)
            {
                model.NumberOfPages++;
            }

            if (model.Page < 1)
            {
                model.Page = 1;
            }
            else if (model.Page > model.NumberOfPages)
            {
                model.Page = model.NumberOfPages;
            }

            PaginationFilter<EmployeeViewModel> paginationFilter = new();
            model.Employees = paginationFilter.FilterItemsByPage(model.Employees, model.Page);

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

        public async Task<List<EmployeeListModel>> GetEmployeeListAsync(string userId)
            => await context
                .Employees
                .Where(e => e.UserId == userId)
                .Select(e => new EmployeeListModel
                {
                    EmployeeId = e.Id,
                    EmployeeName = e.FirstName + " " + e.LastName
                })
                .OrderBy(e => e.EmployeeName)
                .ToListAsync();

        public async Task<EmployeeViewModel> GetEmployeeById(Guid id)
        {
            Employee entity = await context
                .Employees
                .FindAsync(id)
                    ?? throw new InvalidOperationException(EmployeeDoesNotExist);

            return new EmployeeViewModel
            {
                ContractType = entity.ContractType,
                DateEmployed = entity.DateEmployed,
                Egn = entity.Egn,
                EmployeeId = id,
                DateLeft = entity.DateLeft,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MonthlySalary = entity.MonthlySalary
            };
        }

        public async Task<decimal> GetMinimumWageAsync()
        {
            var settingForMinimumWage = await context
                    .GlobalSettings
                    .FindAsync((int)GlobalSetting.MinimumWage);

            if (settingForMinimumWage != null)
            {
                return settingForMinimumWage.SettingValue;
            }
            else
            {
                return 0;
            }
        }
    }
}
