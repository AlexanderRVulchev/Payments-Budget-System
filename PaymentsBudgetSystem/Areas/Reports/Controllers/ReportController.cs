using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Web;
using System.IO;

namespace PaymentsBudgetSystem.Areas.Reports.Controllers
{
    using Core.Models.Report;
    using PaymentsBudgetSystem.Core.Contracts;
    using PaymentsBudgetSystem.Extensions;

    [Authorize]
    [Area("Reports")]
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService _reportService)
        {
            reportService = _reportService;
        }

        [HttpGet]
        public IActionResult ReportInquiry()
        {
            var model = new ReportInquiryViewModel
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportInquiry(ReportInquiryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IndividualReportDataModel reportModel = await reportService.BuildIndividualReport(User.Id(), model.Year, model.Month);

            string templatePath = "wwwroot/Report.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(templatePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];

                if (worksheet != null)
                {
                    worksheet.Cells["E32"].Value = 15;
                    worksheet.Cells["E33"].Value = 3000;

                    using (MemoryStream stream = new MemoryStream())
                    {
                        excelPackage.SaveAs(stream);

                        // Set the position of the memory stream to the beginning
                        stream.Position = 0;

                        // Return the modified Excel file as a downloadable attachment
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                    }
                }

                return View(reportModel);
            }
        }
    }
}
