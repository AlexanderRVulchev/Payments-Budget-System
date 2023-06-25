﻿
namespace PaymentsBudgetSystem.Core.Helpers
{
    using Models.Assets;
    using Models.Enums;
    using PaymentsBudgetSystem.Core.Models.Beneficiaries;
    using PaymentsBudgetSystem.Core.Models.Employees;
    using PaymentsBudgetSystem.Data.Entities;

    public static class Sorter
    {
        public static List<AssetInfoViewModel> SortAssets(
            List<AssetInfoViewModel> assets,
            AssetSort attribute,
            SortBy sortBy)
        {
            if (sortBy == SortBy.Ascending)
            {
                switch (attribute)
                {
                    case AssetSort.Name:
                        assets = assets.OrderBy(a => a.Name).ToList(); break;
                    case AssetSort.AmortizationValue:
                        assets = assets.OrderBy(a => a.Amortization).ToList(); break;
                    case AssetSort.DateAquired:
                        assets = assets.OrderBy(a => a.DateAquired).ToList(); break;
                    case AssetSort.ReportValue:
                        assets = assets.OrderBy(a => a.ReportValue).ToList(); break;
                    case AssetSort.BalanceValue:
                        assets = assets.OrderBy(a => a.BalanceValue).ToList(); break;
                    default: break;
                }
            }
            else
            {
                switch (attribute)
                {
                    case AssetSort.Name:
                        assets = assets.OrderByDescending(a => a.Name).ToList(); break;
                    case AssetSort.AmortizationValue:
                        assets = assets.OrderByDescending(a => a.Amortization).ToList(); break;
                    case AssetSort.DateAquired:
                        assets = assets.OrderByDescending(a => a.DateAquired).ToList(); break;
                    case AssetSort.ReportValue:
                        assets = assets.OrderByDescending(a => a.ReportValue).ToList(); break;
                    case AssetSort.BalanceValue:
                        assets = assets.OrderByDescending(a => a.BalanceValue).ToList(); break;
                    default: break;
                }
            }

            return assets;
        }

        public static IQueryable<Beneficiary> SortBeneficiaries(
            IQueryable<Beneficiary> beneficiaries,
            AllBeneficiariesViewModel model)
        {
            if (model.IdentifierFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Identifier.Contains(model.IdentifierFilter))
                    .AsQueryable();
            }
            if (model.NameFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Name.Contains(model.NameFilter))
                    .AsQueryable();
            }
            if (model.AddressFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Address != null
                            && b.Address.Contains(model.AddressFilter))
                    .AsQueryable();
            }
            if (model.BankAccountFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.BankAccount.Contains(model.BankAccountFilter))
                    .AsQueryable();
            }


            if (model.SortBy == SortBy.Ascending)
            {
                switch (model.SortAttribute)
                {
                    case BeneficiarySort.Name:
                        beneficiaries = beneficiaries.OrderBy(b => b.Name); break;
                    case BeneficiarySort.Identifier:
                        beneficiaries = beneficiaries.OrderBy(b => b.Identifier); break;
                    case BeneficiarySort.Address:
                        beneficiaries = beneficiaries.OrderBy(b => b.Address); break;
                    case BeneficiarySort.BankAccount:
                        beneficiaries = beneficiaries.OrderBy(b => b.BankAccount); break;
                    default: break;
                }
            }
            else
            {
                switch (model.SortAttribute)
                {
                    case BeneficiarySort.Name:
                        beneficiaries = beneficiaries.OrderByDescending(b => b.Name); break;
                    case BeneficiarySort.Identifier:
                        beneficiaries = beneficiaries.OrderByDescending(b => b.Identifier); break;
                    case BeneficiarySort.Address:
                        beneficiaries = beneficiaries.OrderByDescending(b => b.Address); break;
                    case BeneficiarySort.BankAccount:
                        beneficiaries = beneficiaries.OrderByDescending(b => b.BankAccount); break;
                    default: break;
                }
            }

            return beneficiaries;
        }

        public static IQueryable<EmployeeViewModel> SortEmployees(
            IQueryable<EmployeeViewModel> employees,
            AllEmployeesViewModel model)
        {
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

            return employees;
        }
    }
}
