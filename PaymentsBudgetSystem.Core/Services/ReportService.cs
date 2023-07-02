    using System.Threading.Tasks;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.Report;
    using PaymentsBudgetSystem.Core.Helpers;
    using PaymentsBudgetSystem.Core.Models.Information;
    using PaymentsBudgetSystem.Data;
    using PaymentsBudgetSystem.Data.Entities;

    public class ReportService : IReportService
    {
        private readonly PBSystemDbContext context;

        public ReportService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<IndividualReportDataModel> BuildIndividualReport(string userId, int year, int month)
        {
            var payments = await context
                .Payments
                .Where(p => p.UserId == userId)
                .Where(p => p.Date.Year == year)
                .Where(p => p.Date.Month <= month)
                .Include(p => p.SalariesDetails)
                .Select(p => new ReportExpensesDataModel
                {
                    Amount = p.Amount,
                    ParagraphType = p.Paragraph,
                    PaymentType = p.PaymentType,
                    NetSalaryJobContract = p.SalariesDetails.Sum(sd => sd.NetSalaryJobContract),
                    NetSalaryStateOfficial = p.SalariesDetails.Sum(sd => sd.NetSalaryStateOfficial),
                    InsuranceAdditional = p.SalariesDetails.Sum(sd => sd.InsuranceAdditionalEmployee + sd.InsuranceAdditionalEmployer),
                    InsuranceHealth = p.SalariesDetails.Sum(sd => sd.InsuranceHealthEmployee + sd.InsuranceHealthEmployer),
                    InsurancePension = p.SalariesDetails.Sum(sd => sd.InsurancePensionEmployee + sd.InsurancePensionEmployer),
                    IncomeTax = p.SalariesDetails.Sum(sd => sd.IncomeTax)
                })
                .ToListAsync();

            var limits = await context
                .IndividualBudgets
                .Where(ib => ib.UserId == userId)
                .Where(ib => ib.FiscalYear == year)
                .Select(ib => new ReportLimitsDataModel
                {
                    AssetsLimit = ib.AssetsLimit,
                    SalariesLimit = ib.SalariesLimit,
                    SupportLimit = ib.SupportLimit
                })
                .FirstOrDefaultAsync()
                    ?? new ReportLimitsDataModel();

            var model = new IndividualReportDataModel
            {
                Year = year,
                Month = month,
                UserId = userId,
                AssetsLimit = limits.AssetsLimit,
                SalariesLimit = limits.SalariesLimit,
                SupportLimit = limits.SupportLimit
            };

            Calculator calculator = new();

            calculator.CalculateReportExpenses(model, payments);

            return model;
        }

        public async Task SaveIndividualReportAsync(string userId, IndividualReportDataModel model)
        {
            var entry = new Report
            {
                Bank0101 = model.Bank0101,
                Bank0102 = model.Bank0102,
                Bank1015 = model.Bank1015,
                Bank1020 = model.Bank1020,
                Bank1051 = model.Bank1051,
                Bank5100 = model.Bank5100,
                Bank5200 = model.Bank5200,
                Bank5300 = model.Bank5300,
                Cash1015 = model.Cash1015,
                Cash1020 = model.Cash1020,
                Cash1051 = model.Cash1051,
                IsConsolidated = false,
                LimitAssets = model.AssetsLimit,
                LimitSalaries = model.SalariesLimit,
                LimitSupport = model.SupportLimit,
                Transfer0551 = model.Transfer0551,
                Transfer0560 = model.Transfer0560,
                Transfer0580 = model.Transfer0580,
                Transfer0590 = model.Transfer0590,
                UserId = userId,
                Year = model.Year,
                Month = model.Month
            };

            var existingReportForThisMonthAndYear = await context
                    .Reports
                    .Where(r => r.UserId == userId
                        && r.Year == model.Year
                        && r.Month == model.Month
                        && r.IsConsolidated == false)
                    .FirstOrDefaultAsync();
                    
            if (existingReportForThisMonthAndYear != null)
            {
                existingReportForThisMonthAndYear = entry;
            }
            else
            {
                await context.Reports.AddAsync(entry);
            }

            await context.SaveChangesAsync();            
        }
    }
}
