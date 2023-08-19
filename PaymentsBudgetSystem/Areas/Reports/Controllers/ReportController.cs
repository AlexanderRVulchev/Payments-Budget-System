using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Security.Claims;

namespace PaymentsBudgetSystem.Areas.Reports.Controllers
{
    using Core.Models.Report;
    using Core.Contracts;
    using Extensions;

    using static Common.RoleNames;

    [Authorize(Roles = PrimaryAndSecondaryRoleNames)]
    [Area("Reports")]
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService _reportService)
        {
            reportService = _reportService;
        }

        [HttpGet]
        public async Task<IActionResult> ReportInquiry()
        {
            var model = new ReportInquiryViewModel
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month
            };

            await reportService.AddReportAnnotationsAsync(User.Id(), model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportInquiry(ReportInquiryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await reportService.AddReportAnnotationsAsync(User.Id(), model);
                return View(model);
            }

            ReportDataModel reportModel = new();

            try
            {
                if (!model.IsConsolidated)
                {
                    reportModel = await reportService.BuildIndividualReportAsync(User.Id(), model.Year, model.Month);
                }
                else
                {
                    reportModel = await reportService.BuildConsolidatedReportAsync(User.Id(), model.Year, model.Month);
                }
            }
            catch (InvalidOperationException)
            {
                reportModel.Year = model.Year;
                reportModel.Month = model.Month;
            }

            string templatePath = "wwwroot/Report.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(templatePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                if (worksheet != null)
                {
                    reportService.FillCellValuesInWorksheet(worksheet, reportModel);

                    using MemoryStream stream = new();
                    excelPackage.SaveAs(stream);

                    stream.Position = 0;

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FinancialReport.xlsx");
                }
            }
            return View(reportModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveReport(ReportInquiryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ReportInquiry));
            }

            ReportDataModel reportModel = new();

            try
            {
                if (!model.IsConsolidated)
                {
                    reportModel = await reportService.BuildIndividualReportAsync(User.Id(), model.Year, model.Month);
                }
                else
                {
                    reportModel = await reportService.BuildConsolidatedReportAsync(User.Id(), model.Year, model.Month);
                }
            }
            catch (InvalidOperationException)
            {
                reportModel.Year = model.Year;
                reportModel.Month = model.Month;
                reportModel.IsConsolidated = model.IsConsolidated;
            };

            await reportService.SaveIndividualReportAsync(User.Id(), reportModel);

            TempData["SuccessMessage"] = "Отчетът е запазен успешно!";

            return RedirectToAction(nameof(ReportInquiry));
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoadReport(Guid id)
        {
            ReportDataModel model;

            try
            {
                model = await reportService.GetReportByIdAsync(id);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }

            string templatePath = "wwwroot/Report.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(templatePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                if (worksheet != null)
                {
                    reportService.FillCellValuesInWorksheet(worksheet, model);

                    using MemoryStream stream = new();
                    excelPackage.SaveAs(stream);

                    stream.Position = 0;

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FinancialReport.xlsx");
                }
            }

            return RedirectToAction(nameof(ReportInquiry));
        }       
    }
}
