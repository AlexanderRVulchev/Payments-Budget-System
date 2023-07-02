using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            return View(reportModel);
        }
    }
}
