using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models;
    using Core.Models.Report;
    using PaymentsBudgetSystem.Core.Contracts;

    public class HomeController : Controller
    {
        private readonly IReportService reportService;

        private readonly IUserService userService;

        public HomeController(
            IReportService _reportService,
            IUserService _userService)
        {
            reportService = _reportService;
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? id, string? name)
        {
            var reportsToLoad = new ReportInquiryViewModel();

            if (id != null)
            {
                await reportService.AddReportAnnotations(id, reportsToLoad);
            }

            var model = new ReportSelectionViewModel
            {
                SelectedInstitutionId = id,
                SelectedInstitutionName = name,
                ReportAnnotationCollection = reportsToLoad,
                Institutions = await userService.GetAllUsersWithSavedReportsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ReportSelectionViewModel model)
        {
            return RedirectToAction(nameof(Index), new { id = model.SelectedInstitutionId, name = model.SelectedInstitutionName });
        }

        public IActionResult Error(string errorMessage)
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = errorMessage
            });
        }
    }
}