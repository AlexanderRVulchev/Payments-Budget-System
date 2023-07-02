    using System.Threading.Tasks;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models.Report;
    using PaymentsBudgetSystem.Core.Helpers;
    using PaymentsBudgetSystem.Core.Models.Information;
    using PaymentsBudgetSystem.Data;

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
    }
}
