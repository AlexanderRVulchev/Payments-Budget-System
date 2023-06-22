using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Models.Support;
    using Core.Contracts;
    using Extensions;
    using Core.Models.Beneficiaries;

    using static Common.ExceptionMessages.Beneficiary;
    using static Common.ExceptionMessages.Payment;
    using PaymentsBudgetSystem.Data.Entities.Enums;

    [Authorize]
    public class SupportController : Controller
    {
        private readonly IBeneficiaryService beneficiaryService;

        public SupportController(IBeneficiaryService _beneficiaryService)
        {
            beneficiaryService = _beneficiaryService;
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var allUserBeneficiaries = await beneficiaryService
                .GetAllBeneficiariesAsync(User.Id(), new AllBeneficiariesViewModel());

            var model = new NewSupportPaymentViewModel
            {
                Beneficiaries = allUserBeneficiaries.Beneficiaries
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult New(NewSupportPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Payment), new { id = model.SelectedBeneficiary, type = model.SelectedParagraph });
        }

        public async Task<IActionResult> Payment(Guid? id, ParagraphType? type)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = BeneficiaryDoesNotExist });
            }
            if (type == null ||
                new List<ParagraphType> 
                {
                    ParagraphType.Materials1015, 
                    ParagraphType.Services1020, 
                    ParagraphType.BusinessTrips1051
                }.Contains((ParagraphType)type) == false)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = InvalidParagarph });
            }

            BeneficiaryFormModel? beneficiary = null;
            try
            {
                beneficiary = await beneficiaryService.GetBeneficiaryAsync(User.Id(), (Guid)id);
                if (beneficiary == null)
                {
                    throw new InvalidOperationException(BeneficiaryDoesNotExist);
                }
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("Error", "Home", new { area = "", errorMessage = ex.Message });
            }

            var model = new SupportPaymentFormModel
            {
                ParagraphType = (ParagraphType)type,
                Beneficiary = beneficiary
            };

            return View(model);
        }
    }
}
