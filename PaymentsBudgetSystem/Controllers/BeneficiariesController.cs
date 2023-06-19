using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models.Beneficiaries;
    using Core.Models.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Extensions;
    using PaymentsBudgetSystem.Core.Contracts;

    [Authorize]
    public class BeneficiariesController : Controller
    {
        private readonly IBeneficiaryService beneficiaryService;

        public BeneficiariesController(IBeneficiaryService _beneficiaryService)
        {
            this.beneficiaryService = _beneficiaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Info(string? name, string? identifier, string? address, int sortBy, int attribute)
        {
            var model = new AllBeneficiariesViewModel
            {
                SortAttribute = (BeneficiarySort)attribute,
                SortBy = (SortBy)sortBy,
                AddressFilter = address,
                IdentifierFilter = identifier,
                NameFilter = name
            };

            model = await beneficiaryService.GetAllBeneficiariesAsync(User.Id(), model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Info(AllBeneficiariesViewModel model)
        {
            return RedirectToAction(nameof(Info), new
            {
                name = model.NameFilter,
                identifier = model.IdentifierFilter,
                address = model.AddressFilter,
                sortBy = (int)model.SortBy,
                attribute = (int)model.SortAttribute
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new BeneficiaryFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BeneficiaryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await beneficiaryService.AddBeneficiaryAsync(User.Id(), model);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction(nameof(Info));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await beneficiaryService.GetBeneficiaryAsync(User.Id(), id);
                return View(model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BeneficiaryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await beneficiaryService.EditBeneficiary(User.Id(), model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }

            return RedirectToAction(nameof(Info));
        }
    }
}
