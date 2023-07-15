using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Assets;
    using Core.Models.Enums;
    using Extensions;
    using static Common.RoleNames;

    [Authorize(Roles = PrimaryAndSecondaryRoleNames)]
    public class AssetsController : Controller
    {
        private readonly IAssetService assetService;

        public AssetsController(IAssetService _assetService)
        {
            assetService = _assetService;
        }

        public async Task<IActionResult> Info(
            int? year,
            int? month,
            string? name,
            int attribute,
            int sortBy,
            int page)
        {
            year ??= DateTime.Now.Year;
            month ??= DateTime.Now.Month;

            var model = new AllAssetsViewModel
            {
                InfoMonth = (int)month,
                InfoYear = (int)year,
                NameFilter = name,
                SortAttribute = (AssetSort)attribute,
                SortBy = (SortBy)sortBy,
                Page = page
            };

            model = await assetService.GetAllAssetsAsync(User.Id(), model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Info(AllAssetsViewModel model)
        {
            return RedirectToAction(nameof(Info), new
            {
                year = model.InfoYear,
                month = model.InfoMonth,
                name = model.NameFilter,
                attribute = (int)model.SortAttribute,
                sortBy = (int)model.SortBy,
                page = model.Page
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, int year)
        {
            try
            {
                var model = await assetService.GetAssetDetailsAsync(User.Id(), id, year);
                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Details(AssetDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await assetService.GetAssetDetailsAsync(User.Id(), model.AssetId, model.Year);
                return View(model);
            }
            return RedirectToAction(nameof(Details), new { id = model.AssetId, year = model.Year });
        }
    }
}
