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
    using System;

    using static Common.ExceptionMessages.Report;
    using static Common.ExceptionMessages.Payment;

    public class ReportService : IReportService
    {
        private readonly PBSystemDbContext context;

        public ReportService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task AddReportAnnotationsAsync(string userId, ReportInquiryViewModel model)
        {
            var reports = await context
                .Reports
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .ToArrayAsync();

            foreach (var report in reports)
            {
                var reportAnnotationModel = new ReportAnnotationViewModel
                {
                    Month = report.Month,
                    Year = report.Year,
                    ReportId = report.Id
                };                

                if (report.IsConsolidated)
                {
                    model.ConsolidatedReports.Add(reportAnnotationModel);
                }
                else
                {
                    model.IndividualReports.Add(reportAnnotationModel);
                }
            }
        }

        public async Task<ReportDataModel> BuildConsolidatedReportAsync(string userId, int year, int month)
        {
            string[] secondaryUsersIds = await context
                .UsersDependancies
                .Where(ud => ud.PrimaryUserId == userId)
                .Select(ud => ud.SecondaryUserId)
                .ToArrayAsync();

            var individualReports = new List<ReportDataModel>();

            var individualReportOfThePrimaryUser = await BuildIndividualReportAsync(userId, year, month);
            individualReports.Add(individualReportOfThePrimaryUser);

            foreach (string secondaryUserId in secondaryUsersIds)
            {
                var individualReportOfASecondaryUser = await BuildIndividualReportAsync(secondaryUserId, year, month);
                individualReports.Add(individualReportOfASecondaryUser);
            }

            var consolidatedReport = new ReportDataModel
            {
                AssetsLimit = individualReports.Sum(r => r.AssetsLimit),
                Bank0101 = individualReports.Sum(r => r.Bank0101),
                Bank0102 = individualReports.Sum(r => r.Bank0102),
                Bank1015 = individualReports.Sum(r => r.Bank1015),
                Bank1020 = individualReports.Sum(r => r.Bank1020),
                Bank1051 = individualReports.Sum(r => r.Bank1051),
                Bank5100 = individualReports.Sum(r => r.Bank5100),
                Bank5200 = individualReports.Sum(r => r.Bank5200),
                Bank5300 = individualReports.Sum(r => r.Bank5300),
                Cash1015 = individualReports.Sum(r => r.Cash1015),
                Cash1020 = individualReports.Sum(r => r.Cash1020),
                Cash1051 = individualReports.Sum(r => r.Cash1051),
                Month = month,
                SalariesLimit = individualReports.Sum(r => r.SalariesLimit),
                SupportLimit = individualReports.Sum(r => r.SupportLimit),
                Transfer0551 = individualReports.Sum(r => r.Transfer0551),
                Transfer0560 = individualReports.Sum(r => r.Transfer0560),
                Transfer0580 = individualReports.Sum(r => r.Transfer0580),
                Transfer0590 = individualReports.Sum(r => r.Transfer0590),
                Year = year,
                UserId = userId,
                IsConsolidated = true
            };

            return consolidatedReport;
        }

        public async Task<ReportDataModel> BuildIndividualReportAsync(string userId, int year, int month)
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
                    ?? throw new InvalidOperationException(NoBudgetCreated);

            var model = new ReportDataModel
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

        public async Task<ReportDataModel> GetReportByIdAsync(Guid id)
        {
            Report entity = await context
                .Reports
                .FindAsync(id)
                    ?? throw new InvalidOperationException(ReportDoesNotExist);

            return new ReportDataModel
            {
                AssetsLimit = entity.LimitAssets,
                Bank0101 = entity.Bank0101,
                Bank0102 = entity.Bank0102,
                Bank1015 = entity.Bank1015,
                Bank1020 = entity.Bank1020,
                Bank1051 = entity.Bank1051,
                Bank5100 = entity.Bank5100,
                Bank5200 = entity.Bank5200,
                Bank5300 = entity.Bank5300,
                Cash1015 = entity.Cash1015,
                Cash1020 = entity.Cash1020,
                Cash1051 = entity.Cash1051,
                IsConsolidated = entity.IsConsolidated,
                Month = entity.Month,
                SalariesLimit = entity.LimitSalaries,
                SupportLimit = entity.LimitSupport,
                Transfer0551 = entity.Transfer0551,
                Transfer0560 = entity.Transfer0560,
                Transfer0580 = entity.Transfer0580,
                Transfer0590 = entity.Transfer0590,
                UserId = entity.UserId,
                Year = entity.Year
            };
        }          

        public async Task SaveIndividualReportAsync(string userId, ReportDataModel model)
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
                IsConsolidated = model.IsConsolidated,
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
                        && r.IsConsolidated == model.IsConsolidated)
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
