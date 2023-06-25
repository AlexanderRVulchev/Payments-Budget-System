using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.Assets;
    using Core.Models.Enums;
    using Extensions;

    [Authorize]
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
            int sortBy)
        {
            year ??= DateTime.Now.Year;
            month ??= DateTime.Now.Month;

            var model = new AllAssetsViewModel
            {
                InfoMonth = (int)month,
                InfoYear = (int)year,
                NameFilter = name,
                SortAttribute = (AssetSort)attribute,
                SortBy = (SortBy)sortBy
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
                sortBy = (int)model.SortBy
            });
        }

        public async Task<IActionResult> AssetDetails(Guid id)
        {
            return View();
        }
    }
}
