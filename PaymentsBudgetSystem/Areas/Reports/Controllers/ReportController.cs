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

            await reportService.AddReportAnnotations(User.Id(), model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportInquiry(ReportInquiryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await reportService.AddReportAnnotations(User.Id(), model);
                return View(model);
            }

            ReportDataModel reportModel = new();

            try
            {
                if (!model.IsConsolidated)
                {
                    reportModel = await reportService.BuildIndividualReport(User.Id(), model.Year, model.Month);
                }
                else
                {
                    reportModel = await reportService.BuildConsolidatedReport(User.Id(), model.Year, model.Month);
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
                    FillCellValuesInWorksheet(worksheet, reportModel);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        excelPackage.SaveAs(stream);

                        stream.Position = 0;

                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FinancialReport.xlsx");
                    }
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
                    reportModel = await reportService.BuildIndividualReport(User.Id(), model.Year, model.Month);
                }
                else
                {
                    reportModel = await reportService.BuildConsolidatedReport(User.Id(), model.Year, model.Month);
                }
            }
            catch (InvalidOperationException)
            {
                reportModel.Year = model.Year;
                reportModel.Month = model.Month;
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
                model = await reportService.GetReportById(id);
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
                    FillCellValuesInWorksheet(worksheet, model);

                    using (MemoryStream stream = new MemoryStream())
                    {
                        excelPackage.SaveAs(stream);

                        stream.Position = 0;

                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FinancialReport.xlsx");
                    }
                }
            }

            return RedirectToAction(nameof(ReportInquiry));
        }

        private void FillCellValuesInWorksheet(ExcelWorksheet worksheet, ReportDataModel reportModel)
        {
            worksheet.Cells["B13"].Value = User.FindFirstValue(ClaimTypes.Email);

            worksheet.Cells["G25"].Value = reportModel.Bank0101;
            worksheet.Cells["G26"].Value = reportModel.Bank0102;

            worksheet.Cells["I27"].Value = reportModel.Transfer0551;
            worksheet.Cells["I28"].Value = reportModel.Transfer0560;
            worksheet.Cells["I29"].Value = reportModel.Transfer0580;
            worksheet.Cells["I30"].Value = reportModel.Transfer0590;

            worksheet.Cells["H32"].Value = reportModel.Cash1015;
            worksheet.Cells["H33"].Value = reportModel.Cash1020;
            worksheet.Cells["H34"].Value = reportModel.Cash1051;

            worksheet.Cells["G32"].Value = reportModel.Bank1015;
            worksheet.Cells["G33"].Value = reportModel.Bank1020;
            worksheet.Cells["G34"].Value = reportModel.Bank1051;

            worksheet.Cells["G36"].Value = reportModel.Bank5100;
            worksheet.Cells["G37"].Value = reportModel.Bank5200;
            worksheet.Cells["G38"].Value = reportModel.Bank5300;

            worksheet.Cells["E24"].Value = reportModel.SalariesLimit;
            worksheet.Cells["E31"].Value = reportModel.SupportLimit;
            worksheet.Cells["E35"].Value = reportModel.AssetsLimit;
        }
    }
}
