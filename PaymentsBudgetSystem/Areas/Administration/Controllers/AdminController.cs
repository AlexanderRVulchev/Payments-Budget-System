using Microsoft.AspNetCore.Mvc;


namespace PaymentsBudgetSystem.Areas.Administration.Controllers
{
    using Core.Contracts;
    using Core.Models;
    using Core.Models.Administration;
    using Microsoft.AspNetCore.Authorization;
    using PaymentsBudgetSystem.Core.Models.Report;

    using static Common.RoleNames;

    [Area("Administration")]
    [Authorize(Roles = AdminRoleName)]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            GlobalSettingsEditModel model = await adminService.GetGlobalSettingsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(GlobalSettingsEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await adminService.SaveGlobalSettingsAsync(model);

            TempData["SuccessMessage"] = "Настойките са запазени успешно!";

            return RedirectToAction(nameof(Settings));
        }

        public async Task<IActionResult> Reports()
        {
            DeleteReportFormModel model = await adminService.GetAllReportsAsync();

            return View(model);
        }

        public async Task<IActionResult> DeleteReport(Guid id)
        {
            try
            {
                await adminService.DeleteReportById(id);
                TempData["SuccessMessage"] = "Отчетът е изтрит успешно!";

                return RedirectToAction(nameof(Reports));
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }
    }
}
